using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Enums;

namespace WebApi.Models;

public record UpdateUserModel(
    string Email,
    string Username,
    string Name,
    [property: JsonConverter(typeof(SafeNullableDateTimeConverter))]
    DateTime? DateOfBirth,
    Gender? Gender,
    string ProfilePicturePath,
    Guid? CityId);
    
public class SafeNullableDateTimeConverter : JsonConverter<DateTime?>
{
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            if (reader.TokenType == JsonTokenType.String &&
                DateTime.TryParse(reader.GetString(), out var result))
            {
                return result;
            }
        }
        catch { }

        return null;
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteStringValue(value.Value.ToString("o"));
        else
            writer.WriteNullValue();
    }
}
