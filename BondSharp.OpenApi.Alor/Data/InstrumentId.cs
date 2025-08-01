using System.Text.Json.Serialization;
using BonadSharp.OpenApi.Core.Instruments;

namespace BondSharp.OpenApi.Alor.Data;
internal struct InstrumentId
{
    [JsonPropertyName("symbol")]
    public required string Symbol { get; init; }

    [JsonPropertyName("exchange")]
    public required string Exchange { get; init; }

    public static InstrumentId Factory(IInstrument instrument)
    {
        return new InstrumentId()
        {
            Exchange = "MOEX",
            Symbol = instrument.Symbol
        };
    }
}
