using System.Text.Json;
using System.Text.Json.Serialization;

namespace BondSharp.OpenApi.Alor.Data;

internal class SecondsJsonConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetInt64();
        return DateTimeOffset.FromUnixTimeSeconds(value).LocalDateTime;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        var datetimeOffset = new DateTimeOffset(value);
        var unixTimeMilliseconds = datetimeOffset.ToUnixTimeSeconds();
        writer.WriteNumberValue(unixTimeMilliseconds);
    }
}
