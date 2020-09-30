using System.Text;
using System.Text.Json;
using Forum.Application.Abstractions.Serialization;

namespace Forum.Infrastructure.Serialization
{
	public class ByteSerializer : IByteSerializer
	{
		public T Deserialize<T>(byte[] bytes) => JsonSerializer.Deserialize<T>(Encoding.Default.GetString(bytes));

		public byte[] Serialize<T>(T obj) => Encoding.Default.GetBytes(JsonSerializer.Serialize(obj));
	}
}
