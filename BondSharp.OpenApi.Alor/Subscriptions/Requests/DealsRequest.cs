using System.Text.Json;
using System.Text.Json.Serialization;
using BonadSharp.OpenApi.Core.Events;
using BonadSharp.OpenApi.Core.Instruments;
using BondSharp.OpenApi.Alor.Data;
using BondSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Alor.Subscriptions.Requests;
internal class DealsRequest :  MarketDataRequest
{
    public DealsRequest(IInstrument instrument, int depth) : base(instrument)
    {
        Depth = depth;
    }

    [JsonPropertyName("depth")]
    public int Depth { get; }

    public override string OperationCode => "AllTradesGetAndSubscribe";

    public override IEvent GetEvent(JsonElement jsonElement)
    {
        var deal = jsonElement.Deserialize<Deal>()!;
        deal.Delay = DateTime.Now - deal.Timestamp;
        return new DealEvent() { Data = deal!, Instrument = Instrument };
    }
}
