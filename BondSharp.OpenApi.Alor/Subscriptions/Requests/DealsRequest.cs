using System.Text.Json.Serialization;
using BonadSharp.OpenApi.Core.Instruments;

namespace BondSharp.OpenApi.Alor.Subscriptions.Requests;
internal class DealsRequest :  BaseRequest
{
    public DealsRequest(IInstrument instrument, int depth) : base(instrument)
    {
        Depth = depth;
    }

    [JsonPropertyName("depth")]
    public int Depth { get; }

    public override string OperationCode => "AllTradesGetAndSubscribe";
}
