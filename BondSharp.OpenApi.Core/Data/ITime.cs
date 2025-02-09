namespace BonadSharp.OpenApi.Core.Data;
public interface ITime
{
    DateTimeOffset Timestamp { get; }
    DateTimeOffset ReceivedAt { get; }
}
