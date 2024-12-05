using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BonadSharp.OpenApi.Core.Data;
using BondSharp.OpenApi.Alor.Data;

namespace BondSharp.OpenApi.Alor.Deals;

internal class DealsHistory
{
    public int Total { get; set; }

    public required List<Deal> List { get; init; }
}