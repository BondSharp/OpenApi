using System.Text.Json.Serialization;
using BondSharp.OpenApi.Alor.Data;

namespace BondSharp.OpenApi.Alor.Orders.Data;
internal class DeleteRequest : BaseRequest
{
    [JsonPropertyName("orderId")]
    public required string OrderId { get; init; }

    [JsonPropertyName("exchange")]
    public string Exchange => "MOEX";

    [JsonPropertyName("user")]
    public required Portfolio Portfolio { get; init; }
}
