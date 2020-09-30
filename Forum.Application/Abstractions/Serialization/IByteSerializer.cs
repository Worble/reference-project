namespace Forum.Application.Abstractions.Serialization
{
	public interface IByteSerializer
	{
		T Deserialize<T>(byte[] bytes);

		byte[] Serialize<T>(T obj);
	}
}
