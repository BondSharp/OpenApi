using System.Text.Json.Serialization;
using BondSharp.Domain.Instruments;
using BondSharp.OpenApi.Core.Data;

namespace BondSharp.OpenApi.Alor.Instruments;
internal class Option : Instrument, IOption
{
    [JsonPropertyName("pxs")]
    public double StrikePrice { get; set; }

    [JsonPropertyName("osd")]
    public OptionSide Side { get; set; }
}
