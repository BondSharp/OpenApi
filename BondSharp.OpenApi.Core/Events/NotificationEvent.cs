using BonadSharp.OpenApi.Core.Events;
using BondSharp.OpenApi.Core.Data;

namespace BondSharp.OpenApi.Core.Events;
public class NotificationEvent : Event<INotification>
{
    public override string ToString()
    {
        return $"{base.ToString()} Notification {Data.Message}";
    }
}
