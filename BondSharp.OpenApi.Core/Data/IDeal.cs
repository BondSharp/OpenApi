namespace BonadSharp.OpenApi.Core.Data;
public interface IDeal : ITime
{
    public double Price { get; }

    public int Quantity { get; }

    public Side Side { get; }

    public int OpenInterest { get; }
}
