using BondSharp.OpenApi.Alor.Data;

namespace BondSharp.OpenApi.Alor.Deals;

internal class DealsHistory
{
    public int Total { get; set; }

    public required List<Deal> List { get; init; }
}