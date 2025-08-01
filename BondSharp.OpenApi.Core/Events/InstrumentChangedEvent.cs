using BonadSharp.OpenApi.Core.Instruments;
using BondSharp.OpenApi.Core.Data;

namespace BondSharp.OpenApi.Core.Events;
public class InstrumentChangedEvent : IInstrumentDataEvent
{
    public required IInstrumentChanged Data { get; init; }

    public required IInstrument Instrument { get; init; }

    public bool Existing => false;
}
