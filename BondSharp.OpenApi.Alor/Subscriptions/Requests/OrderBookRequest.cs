using System.Text.Json.Serialization;
using BonadSharp.OpenApi.Core.Instruments;

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
}
