using System.Text.Json.Serialization;
using BondSharp.OpenApi.Alor.Data;
using BondSharp.OpenApi.Core.Data;

namespace BondSharp.OpenApi.Alor.Providers;

internal class Candles
{
    [JsonPropertyName("h")]
    public required Candle[] Items { get; init; }

    [JsonConverter(typeof(SecondsJsonConverter))]
    public DateTime? Next { get; set; }

    [JsonConverter(typeof(SecondsJsonConverter))]
    public DateTime? Prev { get; set; }
}