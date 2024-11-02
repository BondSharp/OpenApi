using BondSharp.OpenApi.Core.Data;

namespace BondSharp.OpenApi.Alor.Subscriptions;
internal class Notification : INotification
{
    public DateTimeOffset Timestamp { get; } = DateTime.Now;
    public bool Success { get; }
    public string Message { get; }

    public Notification(int code, string message)
    {
        Success = code == 200;
        Message = $"{message} with code = {code}";
    }
}