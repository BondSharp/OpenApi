using BonadSharp.OpenApi.Core.Events;
using BonadSharp.OpenApi.Core.Instruments;
using BondSharp.OpenApi.Abstract;
using BondSharp.OpenApi.Alor.Subscriptions.Requests;
using BondSharp.OpenApi.Core.AbstractServices;
using Microsoft.Extensions.DependencyInjection;
using Websocket.Client;

namespace BondSharp.OpenApi.Alor.Subscriptions;
internal class SubscriptionsBuilder(IServiceScopeFactory scopeFactory) : IIDataMarketBuilder
{
    private readonly List<BaseRequest> requests = new List<BaseRequest>();
    public IDataMarket Build()
    {
        var scope = scopeFactory.CreateScope();
        var requestsSubscriber = scope.ServiceProvider.GetRequiredService<RequestsSubscriber>();
        requests.ForEach(requestsSubscriber.AddRequest);
        var events = scope.ServiceProvider.GetRequiredService<EventsProvider>();
        var websocketClient = scope.ServiceProvider.GetRequiredService<IWebsocketClient>();
        websocketClient.DisconnectionHappened.Subscribe(_ => requestsSubscriber.Subscribe());
        websocketClient.Start();
        requestsSubscriber.Subscribe();
        return new DataMarket(scope, events);
    }

    public IIDataMarketBuilder SubscribeDeal(IInstrument instrument)
    {
        requests.Add(new DealsRequest(instrument, 0));

        return this;
    }

    public IIDataMarketBuilder SubscribeInstrumentChanged(IInstrument instrument)
    {
        requests.Add(new InstrumentChangedRequest(instrument));

        return this;
    }

    public IIDataMarketBuilder SubscribeOrderBook(IInstrument instrument)
    {
        requests.Add(new OrderBookRequest(instrument, 50));

        return this;
    }
}
