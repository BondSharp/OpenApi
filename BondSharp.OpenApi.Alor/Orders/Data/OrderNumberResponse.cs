using System.Text.Json.Serialization;
using BondSharp.OpenApi.Alor.Data;

namespace BondSharp.OpenApi.Alor.Orders.Data;
internal class OrderNumberResponse : EmptyResponce
{
    [JsonPropertyName("orderNumber")]
    public string OrderId { get; init; } = null!;
}
