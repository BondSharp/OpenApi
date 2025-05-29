namespace BondSharp.OpenApi.Core.Events;
public class NotificationEvent :  IEvent
{
    public required bool Success { get; init; }

    public required string Message { get; init; }

    public override string ToString() => Message;
}
