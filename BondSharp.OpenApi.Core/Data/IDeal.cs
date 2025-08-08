namespace BonadSharp.OpenApi.Core.Data;
public interface IDeal : IMarketData
{
    long Id { get; }
    double Price { get; }
    int Quantity { get; }
    Side Side { get; }
    int OpenInterest { get; }

    string? OrderId { get; }
}
