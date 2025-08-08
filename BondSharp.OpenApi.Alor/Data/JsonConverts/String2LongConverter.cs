using System.Text.Json;
using System.Text.Json.Serialization;

namespace BondSharp.OpenApi.Alor.Data.JsonConverts;
internal class String2LongConverter : JsonConverter<long>
{

    public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();

        return long.Parse(value!);

    }

    public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
