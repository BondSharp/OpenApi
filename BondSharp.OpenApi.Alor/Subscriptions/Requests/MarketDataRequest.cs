using System.Text.Json.Serialization;
using BonadSharp.OpenApi.Core.Instruments;

namespace BondSharp.OpenApi.Alor.Subscriptions.Requests;
internal abstract class MarketDataRequest : SubscriptionRequest
{
    [JsonIgnore]
    public IInstrument Instrument { get; }

    [JsonPropertyName("code")]
    public string Code => Instrument.Symbol;

    public MarketDataRequest(IInstrument instrument)
    {
        Instrument = instrument;
    }
}
