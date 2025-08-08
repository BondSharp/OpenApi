using System.Reactive.Linq;
using BondSharp.OpenApi.Core.Events;
using Websocket.Client;

namespace BondSharp.OpenApi.Alor.Subscriptions;
internal class ReconnectionProvider(SubscriptionClient client) : IObservable<ReconnectionEvent>
{

    public IDisposable Subscribe(IObserver<ReconnectionEvent> observer)
    {

        return client.ReconnectionTypes
            .Where(type => type != ReconnectionType.Initial)
            .Select(Map)
            .Subscribe(observer);
    }

    private ReconnectionEvent Map(ReconnectionType type)
    {
        return new ReconnectionEvent() { Message = type.ToString() };
    }
}
