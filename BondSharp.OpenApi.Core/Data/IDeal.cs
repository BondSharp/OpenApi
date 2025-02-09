namespace BonadSharp.OpenApi.Core.Data;
public interface IDeal : ITime
{
    long Id { get; }
    double Price { get; }
    int Quantity { get; }
    Side Side { get; }
    int OpenInterest { get; }
    bool Existing { get; }
}
