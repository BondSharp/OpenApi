using BonadSharp.OpenApi.Core.Instruments;

namespace BondSharp.OpenApi.Alor.Subscriptions.Requests;
internal class InstrumentChangedRequest : BaseRequest
{
    public InstrumentChangedRequest(IInstrument instrument) : base(instrument)
    {
    }

    public override string OperationCode => "InstrumentsGetAndSubscribeV2";
}
