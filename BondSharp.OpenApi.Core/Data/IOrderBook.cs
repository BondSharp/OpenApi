namespace BonadSharp.OpenApi.Core.Data;

public interface IOrderBook : IMarketData
{
    Offer[] Bids { get; }
    Offer[] Asks { get; }
    bool Existing { get; }
}