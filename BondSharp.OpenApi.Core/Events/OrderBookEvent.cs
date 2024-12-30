using BonadSharp.OpenApi.Core.Data;

namespace BonadSharp.OpenApi.Core.Events;
public class OrderBookEvent : Event<IOrderBook>
{
    public override string ToString()
    {
        var bid = Data.Bids.Select(x=>x.Price).LastOrDefault();
        var ask = Data.Asks.Select(x=>x.Price).FirstOrDefault();
        return $"{base.ToString()} OrderBook bid = {bid} ask = {ask} {Data.Bids.Length}";
    }
}
