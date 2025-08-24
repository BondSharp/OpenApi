using BonadSharp.OpenApi.Core.Instruments;
using BondSharp.OpenApi.Alor.Subscriptions.Requests;
using BondSharp.OpenApi.Core.AbstractServices;
using BondSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Alor.Subscriptions;
internal class EventProvider(
    ReconnectionProvider reconnectionProvider,
    SubscriptionCollection subscriptions,
    SubscriptionClient client

    ) : IClientEventProvider
{

    public IObservable<IEvent> Events => client.Events;

    public IObservable<ReconnectionEvent> Reconnections => reconnectionProvider;

    public Task<TimeSpan> Ping()
    {
        return client.Ping();
    }

    public void AddDeal(IInstrument instrument)
    {
        var request = new DealsRequest(instrument, 0);
        subscriptions.Add(request);
    }

    public void AddInstrumentChanged(IInstrument instrument)
    {
        var request = new InstrumentChangedRequest(instrument);

        subscriptions.Add(request);
    }

    public void AddOrderBook(IInstrument instrument, int depth)
    {
        var request = new OrderBookRequest(instrument, depth);

        subscriptions.Add(request);
    }

    public void AddOwnDeal(string account)
    {
        var request = new OwnDealsRequest() { Account = account };

        subscriptions.Add(request);
    }

    public void AddOrders(string account)
    {
        var request = new OrdersRequest { Account = account };

        subscriptions.Add(request);
    }

    public async Task Subscribe()
    {
        var list = new List<Task>();
        foreach (var subscription in subscriptions.All())
        {
            var task = Subscribe(subscription);
            list.Add(task);
        }

        await Task.WhenAll(list);
    }

    async Task Subscribe(BaseRequest subscription)
    {
        var responce = await client.Send(subscription);
        if (responce.Code != 200)
        {
            throw new Exception(responce.ToString());
        }
    }
}
