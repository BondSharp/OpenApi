using System.Text.Json.Serialization;

namespace BondSharp.OpenApi.Alor.Subscriptions.Requests;
internal abstract class BaseRequest
{
    [JsonPropertyName("token")]
    public string? Token { get; set; }

    [JsonPropertyName("opcode")]
    public abstract string OperationCode { get; }

    [JsonPropertyName("guid")]
    public Guid Guid { get; set; } = Guid.NewGuid();
}
