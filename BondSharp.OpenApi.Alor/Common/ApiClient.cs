using System.Net;
using BondSharp.OpenApi.Alor.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BondSharp.OpenApi.Alor.Common;

internal class ApiClient(TokenAuthorization tokenAuthorization, Settings settings)
{
    private readonly bool isProduction = settings.IsProduction;

    private const string developmentAddress = "https://apidev.alor.ru";
    private const string productionAddress = "https://api.alor.ru";

    public async Task<T> Get<T>(string path)
    {
        return await Get<T>(path, []);
    }

    public async Task<T> Get<T>(string path, QueryBuilder query)
    {
        using var client = CreateClient();
        var uri = GetUri(path, query);
        var response = await client.GetAsync(uri);
        
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"HTTP {response.StatusCode}: {errorContent}");
        }
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"HTTP {response.StatusCode}: {errorContent}");
        }
    
        var result = await response.Content.ReadFromJsonAsync<T>();
        return result!;
    }

    public async IAsyncEnumerable<T> GetValues<T>(string path, QueryBuilder query)
    {
        using var client = CreateClient();

        var uri = GetUri(path, query);
        await foreach (var item in client.GetFromJsonAsAsyncEnumerable<T>(uri))
        {
            yield return item!;
        }
    }

    private Uri GetUri(string path, QueryBuilder queryBuilder)
    {
        using var client = CreateClient();

        var uriBuilder = new UriBuilder(isProduction ? productionAddress : developmentAddress)
        {
            Path = path,
            Query = queryBuilder.ToString()
        };

        return uriBuilder.Uri;
    }

    private HttpClient CreateClient()
    {
        var client = new HttpClient();
        var token = tokenAuthorization.Token();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

        return client;
    }
}