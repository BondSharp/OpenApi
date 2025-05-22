namespace BondSharp.OpenApi.Core.Events;
public class PingEvent : IEvent 
{
    public TimeSpan Delay { get; set; }
}
