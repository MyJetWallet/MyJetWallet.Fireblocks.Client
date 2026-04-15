#nullable enable

namespace MyJetWallet.Fireblocks.Client
{
    /// <summary>
    /// A JsonConverter that gracefully handles unknown enum values
    /// by falling back to UNKNOWN instead of throwing an exception.
    /// This prevents deserialization failures when Fireblocks adds new asset types.
    /// </summary>
    public class FallbackStringEnumConverter : Newtonsoft.Json.JsonConverter
    {
        private readonly Newtonsoft.Json.Converters.StringEnumConverter _inner = new();

        public override bool CanConvert(System.Type objectType) => _inner.CanConvert(objectType);

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object? value, Newtonsoft.Json.JsonSerializer serializer)
        {
            _inner.WriteJson(writer, value, serializer);
        }

        public override object? ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, object? existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            try
            {
                return _inner.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch (Newtonsoft.Json.JsonSerializationException)
            {
                var enumType = System.Nullable.GetUnderlyingType(objectType) ?? objectType;
                if (System.Enum.TryParse(enumType, "UNKNOWN", out var unknownValue))
                    return unknownValue;
                return System.Enum.GetValues(enumType).GetValue(0);
            }
        }
    }
}
