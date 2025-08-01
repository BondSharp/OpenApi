using BonadSharp.OpenApi.Core.Events;
using BonadSharp.OpenApi.Core.Instruments;

namespace BondSharp.OpenApi.Core.Events;
public class SubscribedEvent : IInstrumentEvent
{
    public bool Success { get; init; }
    public required string Message { get; set; }

    public required IInstrument Instrument { get; init; }

    public override string ToString()
    {
        return $"{base.ToString()} {Success} Subscribed";
    }
}
