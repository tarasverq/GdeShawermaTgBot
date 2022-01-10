using System.Text.Json;
using System.Text.Json.Serialization;

namespace GdeShawerma.Core.Helpers;

class NullToFalseConverter : JsonConverter<bool>
{
    // Override default null handling
    public override bool HandleNull => true;

    // Check the type
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(bool);
    }

    // Ignore for this exampke
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        bool a = reader.TokenType switch
        {
            JsonTokenType.True => true,
            JsonTokenType.False => false,
            JsonTokenType.Null => false,
            _ => false
        };

        return a;
    }

    // 
    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}