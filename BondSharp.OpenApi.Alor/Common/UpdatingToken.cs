using BondSharp.OpenApi.Alor.Authorization;
using BondSharp.OpenApi.Alor;
using Microsoft.Extensions.Hosting;

namespace AlorClient;

internal class UpdatingToken : BackgroundService
{
    private readonly TokenAuthorization tokenAuthorization;
    private readonly TimeSpan refreshingTokenTimeout;
    public UpdatingToken(TokenAuthorization tokenAuthorization, Settings settings)
    {
        refreshingTokenTimeout = settings.RefreshingTokenTimeout;
        this.tokenAuthorization = tokenAuthorization;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var timeLeft = await GetTimeLeft();
            await Task.Delay(timeLeft, stoppingToken);
            await tokenAuthorization.UpdateToken();
        }
    }

    private async Task<TimeSpan> GetTimeLeft()
    {
        var token = await tokenAuthorization.TokenAsync();
        var leftTime = refreshingTokenTimeout - (DateTimeOffset.Now - token.Created);
        if (TimeSpan.Zero > leftTime)
        {
            return TimeSpan.Zero;
        }

        return leftTime;
    }
}