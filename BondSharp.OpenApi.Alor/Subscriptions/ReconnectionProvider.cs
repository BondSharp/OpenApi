using System.Reactive.Linq;
using BondSharp.OpenApi.Core.Events;
using Websocket.Client;

namespace BondSharp.OpenApi.Alor.Subscriptions;
internal class ReconnectionProvider(IWebsocketClient client) : IObservable<Reconnection>
{

    public IDisposable Subscribe(IObserver<Reconnection> observer)
    {
 
        return client.ReconnectionHappened
            .Where(x=>x.Type != ReconnectionType.Initial)
            .Select(Map)
            .Subscribe(observer);
    }

    private Reconnection Map(ReconnectionInfo reconnectionInfo)
    {

        return new Reconnection() { Message = reconnectionInfo.Type.ToString() };
    }
}
