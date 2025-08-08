using BondSharp.OpenApi.Core.Data;

namespace BondSharp.OpenApi.Core.Events;
public class OwnDealEvent : IEvent
{
    public required string Symbol { get; init; }
    public required IOwnDeal Data { get; init; }
}
