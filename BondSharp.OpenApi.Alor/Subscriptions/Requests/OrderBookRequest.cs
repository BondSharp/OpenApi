using System.Text.Json;
using System.Text.Json.Serialization;
using BonadSharp.OpenApi.Core.Events;
using BonadSharp.OpenApi.Core.Instruments;
using BondSharp.OpenApi.Alor.Data;
using BondSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Alor.Subscriptions.Requests;
internal class OrderBookRequest : MarketDataRequest
{
    [JsonPropertyName("depth")]
    public int Depth { get; }

    public OrderBookRequest(IInstrument instrument, int depth) : base(instrument)
    {
        Depth = depth;
    }

    [JsonPropertyName("opcode")]
    public override string OperationCode => "OrderBookGetAndSubscribe";


    [JsonPropertyName("frequency")]
    public int Frequency { get; } = 0;

    public override IEvent GetEvent(JsonElement jsonElement)
    {
        var orderBook = jsonElement.Deserialize<OrderBook>()!;
        orderBook.Delay = DateTime.Now - orderBook.Timestamp;
        return new OrderBookEvent { Data = orderBook, Instrument = Instrument, Depth = Depth };
    }
}
