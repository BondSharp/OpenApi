using System.Linq;
using System.Text.Json;
using BonadSharp.OpenApi.Core.AbstractServices;
using BonadSharp.OpenApi.Core.Instruments;
using BondSharp.Domain.Instruments;
using BondSharp.OpenApi.Alor.Common;
using BondSharp.OpenApi.Domain.Instruments;
using Microsoft.AspNetCore.Http.Extensions;

namespace BondSharp.OpenApi.Alor.Instruments;
internal class InstrumentsProvider(ApiClient alorApi) : IInstrumentsProvider
{
    private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

    public IAsyncEnumerable<IInstrument> All()
    {

        var queryBuilder = new QueryBuilder(new Dictionary<string, string>()
        {
            ["offset"] = "0",
            ["limit"] = "9999999",
            ["format"] = "Slim"
        });

        var result = alorApi.GetValues<JsonDocument>("md/v2/Securities/MOEX", queryBuilder);

        return result.Select(Parser);
    }

    public async Task<T> Get<T>(string symbol) where T : IInstrument
    {
        var queryBuilder = new QueryBuilder();
        queryBuilder.Add("format", "Slim");
        var json = await alorApi.Get<JsonDocument>($"md/v2/Securities/MOEX/{symbol}", queryBuilder);
        var instrument = Parser(json);
        return (T)(IInstrument)instrument;
    }

    public IAsyncEnumerable<IOption> GetAmericanOptions()
    {
        var options = GetSecurities(cficode: "OPA")
           .Concat(GetSecurities(cficode: "OCA"))
           .Cast<IOption>();

        return options;
    }

    public IAsyncEnumerable<ICurrency> GetCurrencies()
    {
        return GetSecurities(sector: "CURR").Cast<ICurrency>();
    }

    public IAsyncEnumerable<IOption> GetEuropeanOptions()
    {
        var options = GetSecurities(cficode: "OPE")
        .Concat(GetSecurities(cficode: "OCE"))
        .Cast<IOption>();

        return options;
    }

    public IAsyncEnumerable<IFuture> GetFutures()
    {
        return GetSecurities(cficode: "FF").Cast<IFuture>();
    }

    public IAsyncEnumerable<IOption> GetOptions(IInstrument instrument)
    {
        var type = instrument is IFuture ? "A" : "E";
        var options = GetSecurities(cficode: $"OP{type}", query: instrument.Symbol)
            .Concat(GetSecurities(cficode: $"OC{type}", query: instrument.Symbol))
            .Cast<IOption>();
          //.Where(x => x.UnderlyingSymbol == instrument.Symbol);

        return options;
    }

    public IAsyncEnumerable<IShare> GetShares()
    {
        var shares = GetSecurities(cficode: "ES")
            .Concat(GetSecurities(cficode: "EP"))
            .Cast<Share>();

        return shares;
    }

    private async IAsyncEnumerable<Instrument> GetSecurities(string? cficode = null, string? sector = null, string? query = null)
    {
        var @params = new Dictionary<string, string>()
        {
            ["offset"] = "0",
            ["limit"] = "9999999",
            ["format"] = "Slim",
            ["exchange"] = "MOEX",
        };

        var queryBuilder = new QueryBuilder(@params);
        if (cficode != null)
        {
            queryBuilder.Add("cficode", cficode);
        }

        if (sector != null)
        {
            queryBuilder.Add("sector", sector);
        }

        if (query != null)
        {
            queryBuilder.Add("query", query);
        }

        var result = alorApi.GetValues<JsonDocument>("md/v2/Securities", queryBuilder)
            .Select(Parser);

        await foreach (var item in result)
        {
            item.Cancellation = item.Cancellation.UtcDateTime;
            yield return item;
        }
    
    }

    private Instrument Parser(JsonDocument jsonDocument)
    {
        var cficode = jsonDocument.RootElement.GetProperty("cfi").GetString()!;
        if (cficode.StartsWith("OP") || cficode.StartsWith("OC"))
        {
            return Parser<Option>(jsonDocument);
        }

        if (cficode.StartsWith("FF"))
        {
            return Parser<Future>(jsonDocument);
        }

        if (cficode.StartsWith("ES") || cficode.StartsWith("EP"))
        {
            return Parser<Share>(jsonDocument);
        }

        if (cficode.StartsWith("MRC"))
        {
            return Parser<Currency>(jsonDocument);
        }
        return Parser<Instrument>(jsonDocument);
    }

    private T Parser<T>(JsonDocument jsonDocument)
    {
        return jsonDocument.Deserialize<T>(jsonSerializerOptions)!;
    }
}
