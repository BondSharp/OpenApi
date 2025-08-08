using BondSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Core.Data;
internal class DealAccountEvent : IEvent
{
    public required string Account { get; init; }
    

}
