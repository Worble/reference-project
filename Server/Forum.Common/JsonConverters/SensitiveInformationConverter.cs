using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Forum.Common.JsonConverters
{
	public class SensitiveInformationConverter : JsonConverter<string>
	{
		public override string Read(
			ref Utf8JsonReader reader,
			Type typeToConvert,
			JsonSerializerOptions options)
		{
			return reader.GetString() ?? string.Empty;
		}

		public override void Write(
			Utf8JsonWriter writer,
			string value,
			JsonSerializerOptions options)
		{
			writer.WriteStringValue("[SENSITIVE INFORMATION REMOVED]");
		}
	}
}
