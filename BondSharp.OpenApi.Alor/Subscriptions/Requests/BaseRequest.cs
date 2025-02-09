using System.Text.Json;
using System.Text.Json.Serialization;
using BonadSharp.OpenApi.Core.Instruments;

namespace BondSharp.OpenApi.Alor.Subscriptions.Requests;
internal abstract class BaseRequest
{
    [JsonPropertyName("token")]
    public string? Token { get; set; }

    [JsonPropertyName("format")]
    public string Format => "Slim";

    [JsonPropertyName("opcode")]
    public abstract string OperationCode { get; }

    [JsonPropertyName("guid")]
    public Guid Guid { get;} = Guid.NewGuid();

    [JsonIgnore]
    public IInstrument Instrument { get; }

    [JsonPropertyName("code")]
    public string Code => Instrument.Symbol;

    [JsonPropertyName("exchange")]
    public string Exchange => "MOEX";

    [JsonPropertyName("frequency")]
    public int Frequency { get; } = 1;

    [JsonPropertyName("instrumentGroup")]
    public string instrumentGroup => "TQBR";

    public BaseRequest(IInstrument instrument)
    {
        Instrument = instrument;
    }
}
