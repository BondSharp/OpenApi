using BonadSharp.OpenApi.Core.Events;
using BonadSharp.OpenApi.Core.Instruments;
using BondSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Core.AbstractServices;
public interface IDataMarket
{
    IObservable<IEvent> Events { get; }
    IObservable<Reconnection> Reconnections { get; }
    void SubscribeOrderBook(IInstrument instrument);
    void SubscribeDeal(IInstrument instrument);
    void SubscribeInstrumentChanged(IInstrument instrument);
    void Resubscribe();

}
