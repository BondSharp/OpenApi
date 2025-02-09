using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BonadSharp.OpenApi.Core.Data;

namespace BondSharp.OpenApi.Alor.Data;
internal class Deal : IDeal
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("px")]
    public double Price { get; init; }

    [JsonPropertyName("q")]
    public int Quantity { get; init; }

    [JsonPropertyName("s")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Side Side { get; init; }

    [JsonPropertyName("oi")]
    public int OpenInterest { get; init; }

    [JsonPropertyName("t")]
    [JsonConverter(typeof(TimestampJsonConverter))]
    public DateTimeOffset Timestamp { get; init; }

    [JsonPropertyName("h")]
    public bool Existing { get; init; }
    
    [JsonIgnore]
    public DateTimeOffset ReceivedAt { get; set; }
}
