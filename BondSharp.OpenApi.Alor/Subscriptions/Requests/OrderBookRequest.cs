using System.Text.Json.Serialization;
using BonadSharp.OpenApi.Core.Instruments;

namespace BondSharp.OpenApi.Alor.Subscriptions.Requests;
internal class OrderBookRequest : BaseRequest
{
    [JsonPropertyName("depth")]
    public int Depth { get; }

    public OrderBookRequest(IInstrument instrument, int deal) : base(instrument)
    {
        Depth = deal;
    }

    [JsonPropertyName("opcode")]
    public override string OperationCode => "OrderBookGetAndSubscribe";
}
