using System.Text.Json.Serialization;

namespace BondSharp.OpenApi.Alor.Data;
internal class Portfolio
{

    [JsonPropertyName("portfolio")]
    public required string Number { get; init; }

}
