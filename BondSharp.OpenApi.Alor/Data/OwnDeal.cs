using System.Text.Json.Serialization;
using BonadSharp.OpenApi.Core.Data;
using BondSharp.OpenApi.Alor.Data.JsonConverts;
using BondSharp.OpenApi.Core.Data;

namespace BondSharp.OpenApi.Alor.Data;
internal class OwnDeal : IOwnDeal
{
    [JsonPropertyName("id")]
    [JsonConverter(typeof(String2LongConverter))]
    public required long Id { get; init; }

    [JsonPropertyName("eid")]
    public required string OrderId { get; init; }

    [JsonPropertyName("px")]
    public required double Price { get; init; }

    [JsonPropertyName("qb")]
    public required int Quantity { get; init; }

    [JsonPropertyName("s")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required Side Side { get; init; }

    [JsonPropertyName("d")]
    public required DateTime Timestamp { get; init; }

    [JsonIgnore]
    public  TimeSpan Delay { get; set; }

    [JsonPropertyName("h")]
    public required bool Existing { get; init; }

    [JsonPropertyName("sym")]
    public required string Symbol { get; init; }
}
