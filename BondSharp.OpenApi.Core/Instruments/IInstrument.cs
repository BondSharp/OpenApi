namespace BonadSharp.OpenApi.Core.Instruments;
public interface IInstrument 
{
    string Symbol { get; }
    double PriceStep { get; }
    double Lot { get; }
    string CfiCode { get; }
    DateTimeOffset Cancellation { get;}

    DateTimeOffset Updated { get; }
}
