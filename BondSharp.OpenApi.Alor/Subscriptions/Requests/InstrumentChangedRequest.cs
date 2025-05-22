using BonadSharp.OpenApi.Core.Instruments;

namespace BondSharp.OpenApi.Alor.Subscriptions.Requests;
internal class InstrumentChangedRequest : MarketDataRequest
{
    public InstrumentChangedRequest(IInstrument instrument) : base(instrument)
    {
    }

    public override string OperationCode => "InstrumentsGetAndSubscribeV2";
}
