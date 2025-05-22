using System.Net.NetworkInformation;

namespace BondSharp.OpenApi.Alor.Subscriptions.Requests;
internal class PingRequest : BaseRequest
{
    public override string OperationCode => "ping";
}
