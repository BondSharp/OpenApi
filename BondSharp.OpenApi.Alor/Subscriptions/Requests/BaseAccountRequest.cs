using System.Text.Json.Serialization;

namespace BondSharp.OpenApi.Alor.Subscriptions.Requests;
internal abstract class BaseAccountRequest : SubscriptionRequest
{
    [JsonPropertyName("portfolio")]
    public required string Account { get; init; }

    [JsonPropertyName("skipHistory")]
    public bool SkipHistory { get; init; }
}
