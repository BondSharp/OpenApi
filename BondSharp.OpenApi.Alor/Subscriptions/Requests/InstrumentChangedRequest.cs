using System.Text.Json;
using BonadSharp.OpenApi.Core.Instruments;
using BondSharp.OpenApi.Alor.Data;
using BondSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Alor.Subscriptions.Requests;
internal class InstrumentChangedRequest : MarketDataRequest
{
    public InstrumentChangedRequest(IInstrument instrument) : base(instrument)
    {
    }

    public override string OperationCode => "InstrumentsGetAndSubscribeV2";

    public override IEvent GetEvent(JsonElement jsonElement)
    {
        var instrumentChanged = jsonElement.Deserialize<InstrumentChanged>()!;
        return new InstrumentChangedEvent() { Data = instrumentChanged!, Instrument = Instrument };
    }
}
