using BonadSharp.OpenApi.Core.Data;

namespace BondSharp.OpenApi.Core.Data;
public interface INotification : ITime
{
    string Message { get; }
    bool Success { get; }
}
