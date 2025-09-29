using BonadSharp.OpenApi.Core.Instruments;
using BondSharp.OpenApi.Alor.Common;
using BondSharp.OpenApi.Alor.Data;
using BondSharp.OpenApi.Core.AbstractServices;
using BondSharp.OpenApi.Core.Data;
using Microsoft.AspNetCore.Http.Extensions;

namespace BondSharp.OpenApi.Alor.Providers;

internal class CandleProvider(ApiClient apiClient) : ICandleProvider
{
    public async Task<ICandle[]> Get(IInstrument instrument, DateTime from, DateTime to)
    {
        var fromSeconds = new DateTimeOffset(from).ToUnixTimeSeconds();
        var toSeconds = new DateTimeOffset(to).ToUnixTimeSeconds();
        var builder = new QueryBuilder
        {
            { "symbol", instrument.Symbol },
            { "exchange", "MOEX" },
            { "format", "Slim" },
            { "tf", "W" },
            { "from", toSeconds.ToString() },
            { "to", fromSeconds.ToString() }
        };

        var result = await apiClient.Get<Candles>("/md/v2/history", builder);

        return result.Items.Cast<ICandle>().ToArray();
    }
}