using System.Text.Json;
using System.Text.Json.Serialization;
using BondSharp.OpenApi.Alor.Data;
using BondSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Alor.Subscriptions.Requests;
internal class OwnDealsRequest : BaseAccountRequest
{

    [JsonPropertyName("opcode")]
    public override string OperationCode => "TradesGetAndSubscribeV2";

    public override IEvent GetEvent(JsonElement jsonElement)
    {
        var data = jsonElement.Deserialize<OwnDeal>()!;
        return new OwnDealEvent() { Data = data, Symbol = data.Symbol };
    }
}