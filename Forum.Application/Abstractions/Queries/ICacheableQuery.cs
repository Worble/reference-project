namespace Forum.Application.Abstractions.Queries
{
	public interface ICacheableQuery
	{
		bool BypassCache { get; }

		string CacheKey { get; }

		bool RefreshCachedEntry { get; }

		bool ReplaceCachedEntry { get; }
	}
}
