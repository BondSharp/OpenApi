using System.Text.Json.Serialization;

namespace BondSharp.OpenApi.Alor.Orders.Data;
internal class EmptyResponce
{
    [JsonPropertyName("requestGuid")]
    public required Guid RequestGuid { get; init; }

    [JsonPropertyName("httpCode")]
    public required int HttpCode { get; init; }

    [JsonPropertyName("message")]
    public required string Message { get; init; }
}
