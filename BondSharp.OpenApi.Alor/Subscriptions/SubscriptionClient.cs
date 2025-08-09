using System.Reactive.Linq;
using System.Text.Json;
using BondSharp.OpenApi.Alor.Authorization;
using BondSharp.OpenApi.Alor.Common;
using BondSharp.OpenApi.Alor.Data;
using BondSharp.OpenApi.Alor.Subscriptions.Requests;
using BondSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Alor.Subscriptions;
internal class SubscriptionClient(Settings settings, SubscriptionCollection subscriber, TokenAuthorization authorization) : BaseClient(settings)
{
    protected override string ProductionAddress => "wss://api.alor.ru/ws";

    protected override string DevelopmentAddress => "wss://apidev.alor.ru/ws";


    public IObservable<IEvent> Events => GetMessages(true).Select(Parse);

    public IObservable<EmptyResponce> responces => GetMessages(true).Select(ParseNotification);

    private IObservable<string> GetMessages(bool isResponce)
    {
        return Messages
            .Where(json => json.StartsWith("{\"requestGuid", StringComparison.OrdinalIgnoreCase) == isResponce);
    }

    private EmptyResponce ParseNotification(string json) => JsonSerializer.Deserialize<EmptyResponce>(json)!;

    private IEvent Parse(string json)
    {
        using var jsonDocument = JsonDocument.Parse(json);
        var data = jsonDocument.RootElement.GetProperty("data");
        var guid = jsonDocument.RootElement.GetProperty("guid").GetGuid();
        var subscription = subscriber.FindRequest(guid) ?? throw new Exception($"Not Found Request  {guid}");
        var message = subscription.GetEvent(data);

        return message;
    }

    public async Task<TimeSpan> Ping()
    {
        var datatime = DateTime.Now;
        var ping = new PingRequest();
        var reponce = responces
            .Where(x => x.RequestGuid == ping.Guid)
            .FirstAsync()
            .Timeout(TimeSpan.FromSeconds(3));
        Send(ping);

        await reponce;

        return DateTime.Now - datatime;
    }

    public void Send(BaseRequest request)
    {
        var token = authorization.Token();
        request.Token = token.AccessToken;
        var json = JsonSerializer.Serialize(request, request.GetType());
        Send(json);
    }
}
