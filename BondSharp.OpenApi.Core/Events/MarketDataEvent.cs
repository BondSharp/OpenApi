using BonadSharp.OpenApi.Core.Data;
using BonadSharp.OpenApi.Core.Instruments;

namespace BonadSharp.OpenApi.Core.Events;
public abstract class MarketDataEvent<TData> : IInstrumentEvent, IMarketData where TData : IMarketData
{
    public required TData Data { get; init; }
    public required IInstrument Instrument { get; init; }
    public DateTime Timestamp => Data.Timestamp;
    public DateTime ReceivedAt => Data.ReceivedAt;
    public override string ToString()
    {
        if (Timestamp == default(DateTime))
        {
            return $"{Instrument.Symbol}";
        }
        return $"{Timestamp} {Instrument.Symbol}";
    }
}
