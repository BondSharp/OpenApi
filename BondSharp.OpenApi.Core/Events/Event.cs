using BonadSharp.OpenApi.Core.Data;
using BonadSharp.OpenApi.Core.Instruments;

namespace BonadSharp.OpenApi.Core.Events;
public abstract class Event<TData> : IEvent, ITime where TData : ITime
{
    public required TData Data { get; init; }
    public required IInstrument Instrument { get; init; }

    public DateTimeOffset Timestamp => Data.Timestamp;

    public override string ToString()
    {
        return $"{Timestamp} {Instrument.Symbol}";
    }
}
