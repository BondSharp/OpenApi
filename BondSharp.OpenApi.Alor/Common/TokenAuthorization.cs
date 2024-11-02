using System.Net.Http.Json;
using Microsoft.Extensions.Options;

namespace BondSharp.OpenApi.Alor.Authorization;

internal class TokenAuthorization
{
    private const string developmentAddress = "https://oauthdev.alor.ru";
    private const string productionAddress = "https://oauth.alor.ru";

    private readonly string refreshToken;
    private readonly bool isProduction;
    private Task<Token> token;
    public TokenAuthorization(Settings settings)
    {
        isProduction = settings.IsProduction;
        refreshToken = settings.RefreshToken;
        token = GetAssetTokenAsync();
    }

    public Token Token()
    {
        return token.Result;
    }

    public Task<Token> TokenAsync() => token;

    public async Task UpdateToken()
    {
        var task = GetAssetTokenAsync();
        await task;
        token = task;
    }

    private async Task<Token> GetAssetTokenAsync()
    {
        var baseAddress = new Uri(isProduction ? productionAddress : developmentAddress);

        using (var client = new HttpClient() { BaseAddress = baseAddress })
        {
            var response = await client.PostAsync($"/refresh?token={refreshToken}", null);
            var token = await response.Content.ReadFromJsonAsync<Token>();
            if (token == null)
            {
                throw new NullReferenceException(nameof(token));
            };
            return token;
        }
    }
}