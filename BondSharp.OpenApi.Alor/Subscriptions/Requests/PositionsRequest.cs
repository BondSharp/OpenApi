using System.Text.Json;
using BondSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Alor.Subscriptions.Requests;
internal class PositionsRequest : BaseAccountRequest
{
    public override string OperationCode => "PositionsGetAndSubscribeV2";

    public override IEvent GetEvent(JsonElement jsonElement)
    {
        throw new NotImplementedException();
    }
}
