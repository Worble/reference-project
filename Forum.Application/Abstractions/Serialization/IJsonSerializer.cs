namespace Forum.Application.Abstractions.Serialization
{
	public interface IJsonSerializer
	{
		T Deserialize<T>(string jsonString);

		string Serialize<T>(T obj);
	}
}
