using BonadSharp.OpenApi.Core.Instruments;
using BondSharp.OpenApi.Core.Data;

namespace BondSharp.Domain.Instruments;
public interface  IOption : IInstrument
{
    double StrikePrice { get; }
    OptionSide Side { get; }

}
