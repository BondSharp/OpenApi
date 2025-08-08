using System.Text.Json;
using BondSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Alor.Subscriptions.Requests;
internal class StopOrdersRequest : BaseAccountRequest
{
    public override string OperationCode => "StopOrdersGetAndSubscribeV2";

    public override IEvent GetEvent(JsonElement jsonElement)
    {
        throw new NotImplementedException();
    }
}
