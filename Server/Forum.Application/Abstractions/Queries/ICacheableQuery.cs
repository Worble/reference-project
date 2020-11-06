namespace Forum.Application.Abstractions.Queries
{
	public interface ICacheableQuery
	{
		bool BypassCache => false;
		string CacheKey => GetType().Name;
		bool ReplaceCachedEntry => false;
		public int? SlidingExpirationMinutes => null;
		public int? AbsoluteExpirationMinutes => null;
	}
}
