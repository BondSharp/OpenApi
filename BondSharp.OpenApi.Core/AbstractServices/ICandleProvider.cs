using BonadSharp.OpenApi.Core.Instruments;
using BondSharp.OpenApi.Core.Data;

namespace BondSharp.OpenApi.Core.AbstractServices;

public interface ICandleProvider
{
    Task<ICandle[]> Get(IInstrument instrument, DateTime from, DateTime to);
}