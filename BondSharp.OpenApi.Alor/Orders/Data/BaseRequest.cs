using System.Text.Json.Serialization;

namespace BondSharp.OpenApi.Alor.Orders.Data;
internal abstract class BaseRequest
{

    [JsonPropertyName("opcode")]
    public required string OperationCode { get; init; }

    [JsonPropertyName("guid")]
    public Guid Guid { get; init; } = Guid.NewGuid();
}
