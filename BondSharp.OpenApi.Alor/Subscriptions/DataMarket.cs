using BonadSharp.OpenApi.Core.Events;
using BondSharp.OpenApi.Core.AbstractServices;
using Microsoft.Extensions.DependencyInjection;
using Websocket.Client;

namespace BondSharp.OpenApi.Alor.Subscriptions;
internal class DataMarket(IDisposable disposable, IObservable<IEvent> events) : IDataMarket
{   
    public IObservable<IEvent> Events => events;   
      
    public void Dispose()
    {
        disposable.Dispose();
    }
}
