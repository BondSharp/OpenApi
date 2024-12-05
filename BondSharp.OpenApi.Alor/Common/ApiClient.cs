using BondSharp.OpenApi.Alor.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BondSharp.OpenApi.Alor.Common;
internal class ApiClient
{
    private readonly TokenAuthorization tokenAuthorization;
    private readonly bool isProduction;

    private const string developmentAddress = "https://apidev.alor.ru";
    private const string productionAddress = "https://api.alor.ru";

    public ApiClient(TokenAuthorization tokenAuthorization, Settings settings)
    {
        this.tokenAuthorization = tokenAuthorization;
        isProduction = settings.IsProduction;
    }

    public async Task<T> Get<T>(string path)
    {
        return await Get<T>(path, new QueryBuilder());
    }
    public async Task<T> Get<T>(string path, QueryBuilder query)
    {
        using var client = CreateClient();

        var uri = GetUri(path, query);
        var result = await client.GetFromJsonAsync<T>(uri);
       
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
