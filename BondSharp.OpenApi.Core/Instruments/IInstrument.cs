using BondSharp.OpenApi.Core.Instruments;

namespace BonadSharp.OpenApi.Core.Instruments;
public interface IInstrument : IInstrumentId
{  
    public double PriceStep { get; }
    public double Lot { get; }
    public string CfiCode { get;}
}
