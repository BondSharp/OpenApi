namespace BonadSharp.OpenApi.Core.Data;
public interface IMarketData
{
    DateTime Timestamp { get; }
    DateTime ReceivedAt { get; }

    bool Existing { get; }
}
