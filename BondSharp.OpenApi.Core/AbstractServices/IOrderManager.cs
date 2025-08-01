using BonadSharp.OpenApi.Core.Data;
using BonadSharp.OpenApi.Core.Instruments;
using BondSharp.OpenApi.Core.Data;

namespace BondSharp.OpenApi.Core.AbstractServices;
public interface IOrderManager
{
    Task<string> Market(IInstrument instrument, int volume, Side side, string? oldOrderId);
    Task<string> Limit(IInstrument instrument, int volume, Side side, double price, string? oldOrderId);
    Task<string> Stop(IInstrument instrument, int volume, Side side, ConditionPrice conditionPrice, double triggerPrice, string? oldOrderId);
    Task<string> StopLimit(IInstrument instrument, int volume, Side side, ConditionPrice conditionPrice, double price, double triggerPrice, string? oldOrderId);
    Task DeleteMarket(string orderId);
    Task DeleteLimit(string orderId);
    Task DeleteStop(string orderId);
    Task DeleteStopLimit(string orderId);

    Task LogIn();
}
