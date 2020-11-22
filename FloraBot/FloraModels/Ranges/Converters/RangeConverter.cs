using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FloraModels.Ranges.Converters
{
    public class RangeConverter : JsonConverter<Range>
    {
        public override Range Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var indexes = reader.GetString().Split("..");
            return new Range(int.Parse(indexes[0]), int.Parse(indexes[1]));
        }

        public override void Write(Utf8JsonWriter writer, Range value, JsonSerializerOptions options)
        {
            writer.WriteStringValue($"{value.Start.Value}..{value.End.Value}");
        }
    }
}
