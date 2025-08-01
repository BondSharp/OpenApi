using System.Text.Json.Serialization;
using BonadSharp.OpenApi.Core.Instruments;

namespace BondSharp.OpenApi.Alor.Subscriptions.Requests;
internal abstract class MarketDataRequest : BaseRequest
{
 

    [JsonPropertyName("format")]
    public string Format => "Slim";

    [JsonIgnore]
    public IInstrument Instrument { get; }

    [JsonPropertyName("code")]
    public string Code => Instrument.Symbol;

    [JsonPropertyName("exchange")]
    public string Exchange => "MOEX";

    public MarketDataRequest(IInstrument instrument)
    {
        Instrument = instrument;
    }
}
