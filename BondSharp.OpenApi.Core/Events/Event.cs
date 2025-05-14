using BonadSharp.OpenApi.Core.Data;
using BonadSharp.OpenApi.Core.Instruments;

namespace BonadSharp.OpenApi.Core.Events;
public abstract class Event<TData> : IEvent, ITime where TData : ITime
{
    public required TData Data { get; init; }
    public required IInstrument Instrument { get; init; }
    public DateTimeOffset Timestamp => Data.Timestamp;
    public DateTimeOffset ReceivedAt => Data.ReceivedAt;
    public override string ToString()
    {
        if (Timestamp == default(DateTimeOffset))
        {
            return $"{Instrument.Symbol}";
        }
        return $"{Timestamp} {Instrument.Symbol}";
    }
}
