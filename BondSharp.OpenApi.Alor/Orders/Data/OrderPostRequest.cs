using System.Text.Json.Serialization;
using BonadSharp.OpenApi.Core.Data;
using BondSharp.OpenApi.Alor.Data;
using BondSharp.OpenApi.Core.Data;

namespace BondSharp.OpenApi.Alor.Orders.Data;
internal class OrderPostRequest : BaseRequest
{
    [JsonPropertyName("instrument")]
    public InstrumentId InstrumentId { get; init; }
    [JsonPropertyName("user")]
    public required Portfolio Portfolio { get; init; }

    [JsonPropertyName("side")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required Side Side { get; init; }

    [JsonPropertyName("quantity")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Volume { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("price")]
    public double Price { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("condition")]
    public ConditionPrice? Condition { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("triggerPrice")]
    public double TriggerPrice { get; init; }


    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("orderId")]
    public string? OrderId { get; init; }
}
