using BonadSharp.OpenApi.Core.Data;

namespace BondSharp.OpenApi.Core.Data;

public interface ICandle : IMarketData
{
    public double Close { get; }
    public double Open { get; }
    public double High { get; }
    public double Low { get; }
    public long Volume { get; }
}