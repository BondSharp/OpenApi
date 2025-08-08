using System.Text.Json;
using BondSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Alor.Subscriptions.Requests;
internal class DealsAccountRequest : BaseAccountRequest
{
    public override string OperationCode => "TradesGetAndSubscribeV2";

    public override IEvent GetEvent(JsonElement jsonElement)
    {
        throw new NotImplementedException();
    }
}