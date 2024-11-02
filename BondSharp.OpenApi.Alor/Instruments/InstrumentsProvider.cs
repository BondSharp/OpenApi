using System.Text.Json;
using BonadSharp.OpenApi.Core.AbstractServices;
using BonadSharp.OpenApi.Core.Instruments;
using BondSharp.OpenApi.Alor.Common;
using Microsoft.AspNetCore.Http.Extensions;

namespace BondSharp.OpenApi.Alor.Instruments;
internal class InstrumentsProvider(ApiClient alorApi) : IInstrumentsProvider
{
    private const string cacheFileName = "instruments.json";
    private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };


    public async IAsyncEnumerable<IInstrument> All(TimeSpan duration)
    {
        var сacheFile = new FileInfo(cacheFileName);

        await Download(duration, сacheFile);

        using var stream = сacheFile.OpenRead();
        await foreach (var json in JsonSerializer.DeserializeAsyncEnumerable<JsonDocument>(stream))
        {
            yield return Parser(json!);
        };
    }

    private async Task Download(TimeSpan duration, FileInfo сacheFile)
    {
        if (сacheFile.Exists && сacheFile.LastWriteTimeUtc > (DateTime.UtcNow - duration))
        {
            return;
        }
        if (сacheFile.Exists)
        {
            сacheFile.Delete();
        }

        var queryBuilder = new QueryBuilder(new Dictionary<string, string>()
        {
            ["offset"] = "0",
            ["limit"] = "9999999",
            ["format"] = "Slim"
        });

        var result = await alorApi.Get<JsonElement>("md/v2/Securities/MOEX", queryBuilder);
        var json = result.GetRawText();
        using var write = сacheFile.CreateText();
        await write.WriteAsync(json);
    }


    private Instrument Parser(JsonDocument jsonDocument)
    {
        var cficode = jsonDocument.RootElement.GetProperty("cfi").GetString()!;
        if (cficode.StartsWith("OP") || cficode.StartsWith("OP"))
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
