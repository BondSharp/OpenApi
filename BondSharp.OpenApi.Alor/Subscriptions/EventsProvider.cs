using System.Reactive.Linq;
using System.Text.Json;
using BonadSharp.OpenApi.Core.Data;
using BonadSharp.OpenApi.Core.Events;
using BondSharp.OpenApi.Alor.Data;
using BondSharp.OpenApi.Alor.Subscriptions.Requests;
using BondSharp.OpenApi.Alor.WebSockets;
using BondSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Alor.Subscriptions;
internal class EventsProvider(Subscriber requestsSubscriber, SubscriptionClient client) : IObservable<IEvent>
{
    public IDisposable Subscribe(IObserver<IEvent> observer)
    {
        return client.Messages
            .Select(Parse)
            .Subscribe(observer);
    }

    private IEvent Parse(string json)
    {
        if (json.StartsWith("{ \"data"))
        {
            return ParseMessage(json);
        }
        if (json.StartsWith("{\"requestGuid"))
        {
            var notification = ParseNotification(json);
            return notification;
        }

        throw new ArgumentException(nameof(json));
    }

    private IEvent ParseMessage(string json)
    {
        using var jsonDocument = JsonDocument.Parse(json);

        var data = jsonDocument.RootElement.GetProperty("data");
        var guid = jsonDocument.RootElement.GetProperty("guid").GetGuid();
        var subscription = requestsSubscriber.FindRequest(guid)!;
        var message = ParseMessage(subscription, data);

        return message;

    }

    private IEvent ParseMessage(MarketDataRequest request, JsonElement jsonElement)
    {
        if (request is OrderBookRequest orderBookRequest)
        {
            var orderBook = jsonElement.Deserialize<OrderBook>()!;
            orderBook.Delay = DateTime.Now - orderBook.Timestamp;
            return new OrderBookEvent { Data = orderBook, Instrument = request.Instrument, Depth = orderBookRequest.Depth };
        }

        if (request is DealsRequest)
        {
            var deal = jsonElement.Deserialize<Deal>()!;
            deal.Delay = DateTime.Now - deal.Timestamp;
            return new DealEvent() { Data = deal!, Instrument = request.Instrument };
        }

        if (request is InstrumentChangedRequest)
        {
            var instrumentChanged = jsonElement.Deserialize<InstrumentChanged>()!;
            return new InstrumentChangedEvent() { Data = instrumentChanged!, Instrument = request.Instrument };
        }

        throw new ArgumentException(nameof(request));
    }

    private IEvent ParseNotification(string json)
    {
        var notifaction = JsonSerializer.Deserialize<Notification>(json)!;
        var request = requestsSubscriber.FindRequest(notifaction.RequestGuid);
        if (request != null)
        {
            return new SubscribedEvent() { Success = notifaction.Success, Message = notifaction.ToString(), Instrument = request.Instrument };
        }
        if (requestsSubscriber.TryPingParse(notifaction.RequestGuid, out var pingEvent))
        {
            return pingEvent;
        }
        return new NotificationEvent() { Success = notifaction.Success, Message = notifaction.Message };
    }

    private T Deserialize<T>(JsonElement jsonElement) where T : IMarketData
    {
        return jsonElement.Deserialize<T>()!;
    }
}
