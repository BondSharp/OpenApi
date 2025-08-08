using System.Text.Json;
using BondSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Alor.Subscriptions.Requests;
internal class RisksRequest : BaseAccountRequest
{
    public override string OperationCode => "RisksGetAndSubscribe";

    public override IEvent GetEvent(JsonElement jsonElement)
    {
        throw new NotImplementedException();
    }
}
