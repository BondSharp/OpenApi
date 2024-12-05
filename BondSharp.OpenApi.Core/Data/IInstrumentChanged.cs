using BonadSharp.OpenApi.Core.Data;

namespace BondSharp.OpenApi.Core.Data;

public interface IInstrumentChanged : ITime
{
    TradingStatus? Status { get; }
    double? PriceMin { get; }
    double? PriceMax { get; }

}
