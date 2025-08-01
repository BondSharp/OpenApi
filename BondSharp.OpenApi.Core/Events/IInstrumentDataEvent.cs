using BonadSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Core.Events;
public interface IInstrumentDataEvent : IInstrumentEvent
{
    bool Existing { get; }
}
