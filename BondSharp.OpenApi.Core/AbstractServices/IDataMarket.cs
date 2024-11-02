using BonadSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Core.AbstractServices;
public interface IDataMarket : IDisposable
{
    IObservable<IEvent> Events { get; }
}
