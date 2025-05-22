using System.Text.Json;
using BondSharp.OpenApi.Alor.Authorization;
using BondSharp.OpenApi.Alor.Subscriptions.Requests;
using BondSharp.OpenApi.Core.Events;
using Websocket.Client;

namespace BondSharp.OpenApi.Alor.Subscriptions;
internal class Subscriber(
    TokenAuthorization tokenAuthorization,
    IWebsocketClient client,
    PingService pingService
    )
{
    private readonly Dictionary<Guid, MarketDataRequest> requests = new Dictionary<Guid, MarketDataRequest>();

    public MarketDataRequest? FindRequest(Guid guid)
    {
        return requests.GetValueOrDefault(guid,null);
    }

    public bool TryPingParse(Guid guid, out PingEvent pingEvent)
    {

        if (pingService.TryParse(guid, out var delay))
        {
            pingEvent = new PingEvent() { Delay = delay };
            return true;
        };
        pingEvent = null!;
        return false;

    }

    public void Subscribe(MarketDataRequest request)
    {
        if (requests.ContainsKey(request.Guid))
        {
            throw new ArgumentException($"Duplicate {nameof(request.Guid)} of {nameof(request)}");
        }
        requests.Add(request.Guid, request);

        Send(request);

    }

    public void ReSubscribe()
    {
        foreach (var request in requests.Values)
        {
            Send(request);
        }
    }

    public void SendPing()
    {
        var ping = new PingRequest() { Guid = pingService.CreateGuidRequest()};
        Send(ping);
    }

    private void Send(BaseRequest request)
    {
        var token = tokenAuthorization.Token();
        request.Token = token.AccessToken;
        var json = JsonSerializer.Serialize(request, request.GetType());
        client.Send(json);
    }
}
