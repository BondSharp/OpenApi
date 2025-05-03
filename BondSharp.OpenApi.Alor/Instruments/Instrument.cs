using System.Text.Json.Serialization;
using BonadSharp.OpenApi.Core.Instruments;

namespace BondSharp.OpenApi.Alor.Instruments;

internal class Instrument : IInstrument
{
    [JsonPropertyName("sym")]
    public required string Symbol { get; set; }

    [JsonPropertyName("cfi")]
    public required string CfiCode { get; set; }

    [JsonPropertyName("stp")]
    public required double PriceStep { get; set; }

    [JsonPropertyName("lot")]
    public required double Lot { get; set; }

    [JsonPropertyName("Cncl")]
    public DateTimeOffset Cancellation { get; set; }

    public DateTimeOffset Updated { get; set; } = DateTimeOffset.UtcNow;
}
