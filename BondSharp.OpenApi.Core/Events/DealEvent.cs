using BonadSharp.OpenApi.Core.Data;

namespace BonadSharp.OpenApi.Core.Events;
public class DealEvent : Event<IDeal>
{
    public override string ToString()
    {
        return  $"{base.ToString()} Deal price = {Data.Price} quantity = {Data.Quantity}";
    }
}
