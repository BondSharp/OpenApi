using System.Text.Json.Serialization;

namespace BondSharp.OpenApi.Alor.Subscriptions;
internal class Notification
{
    [JsonPropertyName("requestGuid")]
    public Guid RequestGuid { get; init; }
    [JsonPropertyName("httpCode")]
    public int Code { get; init; }
    [JsonPropertyName("message")]
    public required string Message { get; init; }

    [JsonIgnore]
    public bool Success => 200 == Code;

    public override string ToString()
    {
        return $"{Message} with code = {Code}"; ;
    }
}