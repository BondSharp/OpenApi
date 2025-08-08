using BonadSharp.OpenApi.Core.Instruments;
using BondSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Core.AbstractServices;
public interface IClientEventProvider
{
    IObservable<IEvent> Events { get; }
    IObservable<ReconnectionEvent> Reconnections { get; }
    void AddOrderBook(IInstrument instrument, int depth);
    void AddDeal(IInstrument instrument);
    void AddInstrumentChanged(IInstrument instrument);
    Task<TimeSpan> Ping();

    Task Subscribe();

    void AddOwnDeal(string account);

    void AddOrders(string account);


}
