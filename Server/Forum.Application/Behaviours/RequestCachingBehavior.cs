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
			_dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
			_cache = cache ?? throw new ArgumentNullException(nameof(cache));
			_byteSerializer = byteSerializer ?? throw new ArgumentNullException(nameof(byteSerializer));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
			RequestHandlerDelegate<TResponse> next)
		{
			if (!_options.Value.Enabled
			    || !(request is ICacheableQuery cacheableQuery)
			    || cacheableQuery.BypassCache)
			{
				return await next();
			}

			if (cacheableQuery.ReplaceCachedEntry)
			{
				_logger.LogInformation($"Replacing cache entry for key '{cacheableQuery.CacheKey}'.");
				var response = await next();
				await AddToCache(cancellationToken, response, cacheableQuery);
				return response;
			}

			byte[]? cachedResponse = await _cache.GetAsync(cacheableQuery.CacheKey, cancellationToken);

			if (cachedResponse == null)
			{
				_logger.LogInformation($"Cache miss for key '{cacheableQuery.CacheKey}'. Adding to cache.");
				var response = await next();
				await AddToCache(cancellationToken, response, cacheableQuery);
				return response;
			}

			_logger.LogDebug($"Cache hit for key '{cacheableQuery.CacheKey}'.");
			return _byteSerializer.Deserialize<TResponse>(cachedResponse);
		}

		private Task AddToCache(CancellationToken cancellationToken,
			TResponse response,
			ICacheableQuery cacheableQuery)
		{
			var cacheOptions = new DistributedCacheEntryOptions
			{
				SlidingExpiration = cacheableQuery.SlidingExpirationMinutes.HasValue
					? TimeSpan.FromMinutes(cacheableQuery.SlidingExpirationMinutes.Value)
					: _options.Value.SlidingExpirationMilliseconds.HasValue
						? (TimeSpan?)TimeSpan.FromMilliseconds(_options.Value.SlidingExpirationMilliseconds.Value)
						: null,
				AbsoluteExpiration = cacheableQuery.AbsoluteExpirationMinutes.HasValue
					? _dateTimeService.UtcNowOffset.AddMinutes(cacheableQuery.AbsoluteExpirationMinutes
						.Value)
					: _options.Value.AbsoluteExpirationMilliseconds.HasValue
						? (DateTimeOffset?)_dateTimeService.UtcNowOffset.AddMilliseconds(_options.Value
							.AbsoluteExpirationMilliseconds
							.Value)
						: null
			};
			return _cache.SetAsync(cacheableQuery.CacheKey, _byteSerializer.Serialize(response), cacheOptions,
				cancellationToken);
		}
	}
}
