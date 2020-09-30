using System.Text.Json;
using Forum.Application.Abstractions.Serialization;

namespace Forum.Infrastructure.Serialization
{
	public class SystemJsonSerializer : IJsonSerializer
	{
		public T Deserialize<T>(string jsonString) => JsonSerializer.Deserialize<T>(jsonString);

		public string Serialize<T>(T obj) => JsonSerializer.Serialize(obj);
	}
}
