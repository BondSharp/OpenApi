using BonadSharp.OpenApi.Core.Events;
using BondSharp.OpenApi.Core.Data;

namespace BondSharp.OpenApi.Core.Events;
public class SubscribedEvent : Event<INotification>
{
    public bool Success => Data.Success;
    public override string ToString()
    {
        return $"{base.ToString()} Subscribed";
    }
}
