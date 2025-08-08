using System.Text.Json;
using BondSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Alor.Subscriptions.Requests;
internal class RiskDerivativesRequest : BaseAccountRequest
{
    public override string OperationCode => "SpectraRisksGetAndSubscribe";

    public override IEvent GetEvent(JsonElement jsonElement)
    {
        throw new NotImplementedException();
    }
}
