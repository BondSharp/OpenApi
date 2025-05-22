using BonadSharp.OpenApi.Core.Events;
using BonadSharp.OpenApi.Core.Instruments;
using BondSharp.OpenApi.Core.Data;

namespace BondSharp.OpenApi.Core.Events;
public class InstrumentChangedEvent : IInstrumentEvent
{
    public required IInstrumentChanged Data { get; init; }

    public required IInstrument Instrument { get; init; }
}
