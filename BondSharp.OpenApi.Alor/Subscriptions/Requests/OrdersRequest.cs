using System.Text.Json;
using BondSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Alor.Subscriptions.Requests;
internal class OrdersRequest : BaseAccountRequest
{
    public override string OperationCode => "OrdersGetAndSubscribeV2";

    public override IEvent GetEvent(JsonElement jsonElement)
    {
        return new OrderEvent();
    }
}
