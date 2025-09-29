using System.Text.Json.Serialization;
using BondSharp.OpenApi.Core.Data;

namespace BondSharp.OpenApi.Alor.Data;

internal class Candle : ICandle
{
    [JsonPropertyName("t")]
    [JsonConverter(typeof(SecondsJsonConverter))]
    public DateTime Timestamp { get; init; }

    [JsonIgnore]
    public TimeSpan Delay { get; init; } = TimeSpan.Zero;

    [JsonIgnore]
    public bool Existing { get; init; } = false;
    [JsonPropertyName("c")]
    public double Close { get; init; }
    [JsonPropertyName("o")]
    public double Open { get; init; }
    [JsonPropertyName("h")]
    public double High { get; init; }
    [JsonPropertyName("l")]
    public double Low { get; init; }
    [JsonPropertyName("v")]
    public long Volume { get; init; }
}