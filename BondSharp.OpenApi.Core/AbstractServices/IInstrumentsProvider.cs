using System.Diagnostics.Metrics;
using BonadSharp.OpenApi.Core.Instruments;

namespace BonadSharp.OpenApi.Core.AbstractServices;

public interface IInstrumentsProvider
{
    IAsyncEnumerable<IInstrument> All(TimeSpan duration);
}
