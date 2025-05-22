using BonadSharp.OpenApi.Core.Data;

namespace BondSharp.OpenApi.Core.Data;

public interface IInstrumentChanged
{
    TradingStatus? Status { get; }
    double? PriceMin { get; }
    double? PriceMax { get; }
    DateTime ReceivedAt { get;}

}
