using System.Reactive.Linq;
using BondSharp.OpenApi.Core.Events;
using Websocket.Client;

namespace BondSharp.OpenApi.Alor.Subscriptions;
internal class ReconnectionProvider(IWebsocketClient client) : IObservable<ReconnectionEvent>
{

    public IDisposable Subscribe(IObserver<ReconnectionEvent> observer)
    {
 
        return client.ReconnectionHappened
            .Where(x=>x.Type != ReconnectionType.Initial)
            .Select(Map)
            .Subscribe(observer);
    }

    private ReconnectionEvent Map(ReconnectionInfo reconnectionInfo)
    {

        return new ReconnectionEvent() { Message = reconnectionInfo.Type.ToString() };
    }
}
