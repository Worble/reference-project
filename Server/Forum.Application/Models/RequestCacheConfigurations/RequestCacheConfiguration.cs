namespace Forum.Application.Models.RequestCacheConfigurations
{
	public class RequestCacheConfiguration
	{
		public bool Enabled { get; set; }
		public long? SlidingExpirationMilliseconds { get; set; }
		public long? AbsoluteExpirationMilliseconds { get; set; }
	}
}
