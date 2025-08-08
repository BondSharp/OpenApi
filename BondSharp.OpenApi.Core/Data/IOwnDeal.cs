using BonadSharp.OpenApi.Core.Data;

namespace BondSharp.OpenApi.Core.Data;
public interface IOwnDeal : IMarketData
{
    long Id { get; }
    string? OrderId { get; }
    double Price { get; }
    int Quantity { get; }
    Side Side { get; }

}
