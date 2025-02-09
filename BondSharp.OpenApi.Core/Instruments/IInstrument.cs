namespace BonadSharp.OpenApi.Core.Instruments;
public interface IInstrument 
{
    public string Symbol { get; }
    public double PriceStep { get; }
    public double Lot { get; }
    public string CfiCode { get; }
}
