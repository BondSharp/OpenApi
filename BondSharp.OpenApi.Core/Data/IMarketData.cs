namespace BonadSharp.OpenApi.Core.Data;
public interface IMarketData
{
    DateTime Timestamp { get; }
    TimeSpan Delay { get; }

    bool Existing { get; }
}
