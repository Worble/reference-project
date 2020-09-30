using System;
using System.Threading;
using System.Threading.Tasks;
using Forum.Application.Abstractions.Dates;
using Forum.Application.Abstractions.Queries;
using Forum.Application.Abstractions.Serialization;
using Forum.Application.Models.RequestCacheConfigurations;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Forum.Application.Behaviours
{
	public class RequestCachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : notnull
	{
		private readonly IByteSerializer _byteSerializer;
		private readonly IDistributedCache _cache;
		private readonly IDateTimeService _dateTimeService;
		private readonly ILogger<TResponse> _logger;
		private readonly IOptionsSnapshot<RequestCacheConfiguration> _options;

		public RequestCachingBehavior(IDistributedCache cache, IByteSerializer byteSerializer,
			ILogger<TResponse> logger, IOptionsSnapshot<RequestCacheConfiguration> options,
			IDateTimeService dateTimeService)
		{
			_options = options ?? throw new ArgumentNullException(nameof(options));
			_dateTimeService = dateTimeService;
			_cache = cache ?? throw new ArgumentNullException(nameof(cache));
			_byteSerializer = byteSerializer ?? throw new ArgumentNullException(nameof(byteSerializer));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
			RequestHandlerDelegate<TResponse> next)
		{
			if (!_options.Value.Enabled || !(request is ICacheableQuery cacheableQuery))
			{
				return await next();
			}

			TResponse response;

			if (cacheableQuery.ReplaceCachedEntry)
			{
				_logger.LogInformation($"Replacing cache entry for key '{cacheableQuery.CacheKey}'.");
				response = await GetResponseAndAddToCache(cancellationToken, next, cacheableQuery);
			}
			else
			{
				byte[]? cachedResponse = await _cache.GetAsync(cacheableQuery.CacheKey, cancellationToken);
				if (cachedResponse != null)
				{
					_logger.LogInformation($"Cache hit for key '{cacheableQuery.CacheKey}'.");
					response = _byteSerializer.Deserialize<TResponse>(cachedResponse);
				}
				else
				{
					_logger.LogInformation($"Cache miss for key '{cacheableQuery.CacheKey}'. Adding to cache.");
					response = await GetResponseAndAddToCache(cancellationToken, next, cacheableQuery);
				}
			}

			return response;
		}

		private async Task<TResponse> GetResponseAndAddToCache(CancellationToken cancellationToken,
			RequestHandlerDelegate<TResponse> next,
			ICacheableQuery cacheableQuery)
		{
			TResponse response = await next();

			var cacheOptions = new DistributedCacheEntryOptions
			{
				SlidingExpiration = _options.Value.SlidingExpirationMinutes.HasValue
					? (TimeSpan?)TimeSpan.FromMinutes(_options.Value.SlidingExpirationMinutes.Value)
					: null,
				AbsoluteExpiration = _options.Value.AbsoluteExpirationMinutes.HasValue
					? (DateTimeOffset?)_dateTimeService.UtcNowOffset.AddMinutes(_options.Value.AbsoluteExpirationMinutes
						.Value)
					: null
			};
			await _cache.SetAsync(cacheableQuery.CacheKey, _byteSerializer.Serialize(response), cacheOptions,
				cancellationToken);

			return response;
		}
	}
}
