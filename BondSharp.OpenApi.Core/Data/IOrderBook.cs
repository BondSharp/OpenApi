namespace BonadSharp.OpenApi.Core.Data;

public interface IOrderBook : ITime
{
    Offer[] Bids { get; }

    Offer[] Asks { get; }
    
}