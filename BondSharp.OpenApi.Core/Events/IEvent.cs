using BonadSharp.OpenApi.Core.Data;
using BonadSharp.OpenApi.Core.Instruments;

namespace BonadSharp.OpenApi.Core.Events;
public interface IEvent : ITime
{
    IInstrument Instrument { get; }
    
}
