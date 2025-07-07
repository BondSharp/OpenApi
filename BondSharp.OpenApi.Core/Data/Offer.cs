using System.Diagnostics.CodeAnalysis;

namespace BonadSharp.OpenApi.Core.Data;
public struct Offer : IComparable<Offer>, IEquatable<Offer>
{
    public double Price { get; set; }
    public int Volume { get; set; }

    public Offer(double price, int volume)
    {
        Price = price;
        Volume = volume;
    }

    public int CompareTo(Offer other)
    {
        return Price.CompareTo(other.Price);
    }

    public bool Equals(Offer other)
    {
        return this.Price == other.Price && this.Volume == other.Volume;
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is Offer offer)
        {
            return Equals(offer);
        }
        return base.Equals(obj);
    }
}
