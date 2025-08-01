using System.Text.Json.Serialization;

namespace BondSharp.OpenApi.Alor.Orders.Data;
internal class AuthorizeRequest : BaseRequest
{

    [JsonPropertyName("token")]
    public required string Token { get; init; }
}
