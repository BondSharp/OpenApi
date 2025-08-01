using BonadSharp.OpenApi.Core.Instruments;
using BondSharp.Domain.Instruments;
using BondSharp.OpenApi.Domain.Instruments;

namespace BonadSharp.OpenApi.Core.AbstractServices;

public interface IInstrumentsProvider
{
    IAsyncEnumerable<IInstrument> All();
    IAsyncEnumerable<IShare> GetShares();
    IAsyncEnumerable<ICurrency> GetCurrencies();
    IAsyncEnumerable<IFuture> GetFutures();
    IAsyncEnumerable<IOption> GetAmericanOptions();
    IAsyncEnumerable<IOption> GetEuropeanOptions();
    Task<T> Get<T>(string symbol) where T : IInstrument;
}