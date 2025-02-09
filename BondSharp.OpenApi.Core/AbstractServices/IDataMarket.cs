using BonadSharp.OpenApi.Core.Events;
using BonadSharp.OpenApi.Core.Instruments;

namespace BondSharp.OpenApi.Core.AbstractServices;
public interface IDataMarket
{
    IObservable<IEvent> Events { get; }
    void SubscribeOrderBook(IInstrument instrument);
    void SubscribeDeal(IInstrument instrument);
    void SubscribeInstrumentChanged(IInstrument instrument);
}
