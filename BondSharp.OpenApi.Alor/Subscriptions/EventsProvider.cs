using System.Reactive.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using BonadSharp.OpenApi.Core.Data;
using BonadSharp.OpenApi.Core.Events;
using BondSharp.OpenApi.Alor.Data;
using BondSharp.OpenApi.Alor.Subscriptions.Requests;
using BondSharp.OpenApi.Core.AbstractServices;
using BondSharp.OpenApi.Core.Data;
using BondSharp.OpenApi.Core.Events;
using Websocket.Client;

namespace BondSharp.OpenApi.Alor.Subscriptions;
internal class EventsProvider(Subscriber requestsSubscriber, IWebsocketClient client) : IObservable<IEvent>
{

    public IDisposable Subscribe(IObserver<IEvent> observer)
    {
        return client.MessageReceived
            .Select(Parse)
            .Subscribe(observer);
    }

    private IEvent Parse(ResponseMessage responseMessage)
    {
        if (responseMessage.Text!.StartsWith("{ \"data"))
        {
            ParseMessage(responseMessage);
            return ParseMessage(responseMessage);
        }
        if (responseMessage.Text.StartsWith("{\"requestGuid"))
        {
            var notification = ParseNotification(responseMessage);
            return notification;
        }

        throw new ArgumentException(nameof(responseMessage));
    }

    private IEvent ParseMessage(ResponseMessage responseMessage)
    {
        using var jsonDocument = JsonDocument.Parse(responseMessage.Text!);

        var data = jsonDocument.RootElement.GetProperty("data");
        var guid = jsonDocument.RootElement.GetProperty("guid").GetGuid();
        var subscription = requestsSubscriber.GetRequest(guid);
        var message = ParseMessage(subscription, data);

        return message;

    }

    private IEvent ParseMessage(BaseRequest request, JsonElement jsonElement)
    {
        if (request is OrderBookRequest)
        {
            var z = jsonElement.GetRawText();
            var orderBook = jsonElement.Deserialize<OrderBook>()!;
            orderBook.ReceivedAt = DateTimeOffset.UtcNow;
            return new OrderBookEvent { Data = orderBook, Instrument = request.Instrument };
        }

        if (request is DealsRequest)
        {
            var deal = jsonElement.Deserialize<Deal>()!;
            deal.ReceivedAt = DateTimeOffset.UtcNow;
            return new DealEvent() { Data = deal!, Instrument = request.Instrument };
        }

        if (request is InstrumentChangedRequest)
        {
            var instrumentChanged = jsonElement.Deserialize<InstrumentChanged>()!;
            instrumentChanged.ReceivedAt = DateTimeOffset.UtcNow;
            return new InstrumentChangedEvent() { Data = instrumentChanged!, Instrument = request.Instrument };
        }

        throw new ArgumentException(nameof(request));
    }

    private IEvent ParseNotification(ResponseMessage responseMessage)
    {
        using var jsonDocument = JsonDocument.Parse(responseMessage.Text!);
        var guid = jsonDocument.RootElement.GetProperty("requestGuid").GetGuid();
        var code = jsonDocument.RootElement.GetProperty("httpCode").GetInt32();
        var message = jsonDocument.RootElement.GetProperty("message").GetString() ?? throw new NullReferenceException();
        var request = requestsSubscriber.GetRequest(guid);
        var notification = new Notification(code, message);
        notification.ReceivedAt = DateTimeOffset.UtcNow;
        return new SubscribedEvent() { Data = notification, Instrument = request.Instrument };
    }

    private T Deserialize<T>(JsonElement jsonElement) where T : ITime
    {
        return jsonElement.Deserialize<T>()!;
    }
}
