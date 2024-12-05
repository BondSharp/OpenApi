using System.Text.Json;
using System.Text.Json.Serialization;
using BonadSharp.OpenApi.Core.Data;

namespace BondSharp.OpenApi.Alor.Data.JsonConvert;
internal class OffersJsonConverter : JsonConverter<Offer[]>
{
    public override Offer[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var result = new List<Offer>(20);
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                break;
            }
            using var document = JsonDocument.ParseValue(ref reader);
            var price = document.RootElement.GetProperty("p").GetDouble();
            var volume = document.RootElement.GetProperty("v").GetInt32();
            var offer = new Offer() { Price = price, Volume = volume };
            result.Add(offer);
        }

        return result.ToArray();
    }

    public override void Write(Utf8JsonWriter writer, Offer[] value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
