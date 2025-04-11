using System.Text.Json;
using BonadSharp.OpenApi.Core.Events;
using BonadSharp.OpenApi.Core.Instruments;
using BondSharp.OpenApi.Alor.Authorization;
using BondSharp.OpenApi.Alor.Subscriptions.Requests;
using BondSharp.OpenApi.Core.AbstractServices;
using BondSharp.OpenApi.Core.Events;
using Microsoft.Extensions.DependencyInjection;
using Websocket.Client;

namespace BondSharp.OpenApi.Alor.Subscriptions;
internal class DataMarket(EventsProvider eventsProvider, ReconnectionProvider reconnectionProvider, Subscriber requestsSubscriber) : IDataMarket
{
    
    public IObservable<IEvent> Events => eventsProvider;

    public IObservable<Reconnection> Reconnections => reconnectionProvider;

    public void Resubscribe()
    {
        requestsSubscriber.ReSubscribe();
    }

    public void SubscribeDeal(IInstrument instrument)
    {
        var request = new DealsRequest(instrument, 0);
        requestsSubscriber.Subscribe(request);
    }

    public void SubscribeInstrumentChanged(IInstrument instrument)
    {
        var request = new InstrumentChangedRequest(instrument);

        requestsSubscriber.Subscribe(request);
    }

    public void SubscribeOrderBook(IInstrument instrument)
    {
        var request = new OrderBookRequest(instrument,20);

        requestsSubscriber.Subscribe(request);
    }
}
