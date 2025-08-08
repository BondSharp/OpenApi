
using System.Reactive.Linq;
using BonadSharp.OpenApi.Core.Instruments;
using BondSharp.OpenApi.Alor.Data;
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

    public IObservable<IEvent> Events => client.GetEvents();

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
        var count = subscriptions.Count();
        var reponces = client.GetNotification().Take(count).ToArray().Timeout(TimeSpan.FromSeconds(5));
        foreach (var subscription in subscriptions.All())
        {
            client.Send(subscription);
        }
        Checks(await reponces);
    }

    private void Checks(EmptyResponce[] emptyResponces)
    {
        var guidInResponces = emptyResponces.ToDictionary(x => x.RequestGuid);

        foreach (var subscription in subscriptions.All())
        {
            if (guidInResponces.TryGetValue(subscription.Guid, out EmptyResponce? responce))
            {
                if (responce.Code != 200)
                {
                    throw new Exception(responce.ToString());
                }
            }
            else
            {
                throw new Exception($"Not Found Guid {subscription.Guid} ");
            }
        }
    }
}
