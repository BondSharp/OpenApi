using BonadSharp.OpenApi.Core.Events;
using BonadSharp.OpenApi.Core.Instruments;
using BondSharp.OpenApi.Core.AbstractServices;

namespace BondSharp.OpenApi.Abstract;
public interface IIDataMarketBuilder 
{
    IIDataMarketBuilder SubscribeOrderBook(IInstrument instrument);
    IIDataMarketBuilder SubscribeDeal(IInstrument instrument);
    IDataMarket Build();
}
