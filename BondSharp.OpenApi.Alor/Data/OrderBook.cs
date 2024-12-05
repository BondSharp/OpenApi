using System.Text.Json.Serialization;
using BonadSharp.OpenApi.Core.Data;
using BondSharp.OpenApi.Alor.Data.JsonConvert;

namespace BondSharp.OpenApi.Alor.Data;
internal class OrderBook : IOrderBook
{
    [JsonPropertyName("b")]
    [JsonConverter(typeof(OffersJsonConverter))]
    public required Offer[] Bids { get; init; }

    [JsonPropertyName("a")]
    [JsonConverter(typeof(OffersJsonConverter))]
    public required Offer[] Asks { get; init; }

    [JsonPropertyName("t")]
    [JsonConverter(typeof(TimestampJsonConverter))]
    public DateTimeOffset Timestamp { get; set; }

}
