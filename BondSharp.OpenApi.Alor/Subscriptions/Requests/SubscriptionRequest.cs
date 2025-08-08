using System.Text.Json;
using System.Text.Json.Serialization;
using BondSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Alor.Subscriptions.Requests;
internal abstract class SubscriptionRequest : BaseRequest
{

    [JsonPropertyName("format")]
    public string Format => "Slim";

    [JsonPropertyName("exchange")]
    public string Exchange => "MOEX";

    public abstract IEvent GetEvent(JsonElement jsonElement);
}
