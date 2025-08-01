using BonadSharp.OpenApi.Core.Instruments;
using BondSharp.OpenApi.Core.Events;

namespace BonadSharp.OpenApi.Core.Events;
public interface IInstrumentEvent : IEvent
{
    IInstrument Instrument { get; }    
}
