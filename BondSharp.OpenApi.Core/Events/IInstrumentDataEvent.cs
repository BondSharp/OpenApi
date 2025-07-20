using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BonadSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Core.Events;
public interface IInstrumentDataEvent : IInstrumentEvent
{
    bool Existing { get; }
}
