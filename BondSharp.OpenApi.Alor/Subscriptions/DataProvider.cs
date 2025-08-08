using System.Reactive.Linq;
using System.Text.Json;
using BondSharp.OpenApi.Alor.WebSockets;
using BondSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Alor.Subscriptions;
internal class DataProvider(Subscriber requestsSubscriber, SubscriptionClient client)
{
    public IObservable<IEvent> Events()
    {
        return client.Messages
              .Where(json => json.StartsWith("{ \"data"))
            .Select(Parse);
    }

    private IEvent Parse(string json)
    {
        using var jsonDocument = JsonDocument.Parse(json);

        var data = jsonDocument.RootElement.GetProperty("data");
        var guid = jsonDocument.RootElement.GetProperty("guid").GetGuid();
        var subscription = requestsSubscriber.FindRequest(guid) ?? throw new Exception($"Not Found Request  {guid}");
        var message = subscription.GetEvent(data);

        return message;
    }
}
