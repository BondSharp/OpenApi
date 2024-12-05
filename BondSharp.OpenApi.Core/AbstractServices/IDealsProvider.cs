using BonadSharp.OpenApi.Core.Data;
using BonadSharp.OpenApi.Core.Instruments;

namespace BondSharp.OpenApi.Core.AbstractServices;
public interface IDealsProvider
{
    IAsyncEnumerable<IDeal> GetToday(IInstrument instrument, IDeal? withDeal = null, bool descending = false, int batchSize = 50000);

    IAsyncEnumerable<IDeal> GetPast(IInstrument instrument, DateTimeOffset? from = null, DateTimeOffset? to = null, int batchSize = 50000);
}
