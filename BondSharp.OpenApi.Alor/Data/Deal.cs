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
    public DateTime Timestamp { get; init; }

    [JsonPropertyName("h")]
    public bool Existing { get; set; }

    [JsonIgnore]
    public TimeSpan Delay { get; set; } = TimeSpan.Zero;

    [JsonPropertyName("eid")]
    public string? OrderId { get; init; }
}
