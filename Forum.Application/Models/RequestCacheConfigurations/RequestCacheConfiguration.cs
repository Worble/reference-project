namespace Forum.Application.Models.RequestCacheConfigurations
{
	public class RequestCacheConfiguration
	{
		public bool Enabled { get; }
		public int? SlidingExpirationMinutes { get; }
		public int? AbsoluteExpirationMinutes { get; }
	}
}
