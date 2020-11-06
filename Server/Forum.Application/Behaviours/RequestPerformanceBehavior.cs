using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Forum.Application.Abstractions.Identity;
using Forum.Application.Abstractions.Serialization;
using Forum.Application.Models.RequestPerformanceConfigurations;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Forum.Application.Behaviours
{
	public class RequestPerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : notnull
	{
		private readonly ICurrentUserService _currentUserService;
		private readonly IJsonSerializer _jsonSerializer;
		private readonly ILogger<TRequest> _logger;
		private readonly IOptionsSnapshot<RequestPerformanceConfiguration> _optionsSnapshot;
		private readonly Stopwatch _timer;

		public RequestPerformanceBehavior(ILogger<TRequest> logger, ICurrentUserService currentUserService,
			IOptionsSnapshot<RequestPerformanceConfiguration> optionsSnapshot, IJsonSerializer jsonSerializer)
		{
			_timer = new Stopwatch();
			_logger = logger;
			_currentUserService = currentUserService;
			_optionsSnapshot = optionsSnapshot;
			_jsonSerializer = jsonSerializer;
		}

		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
			RequestHandlerDelegate<TResponse> next)
		{
			if (!_optionsSnapshot.Value.Enabled)
			{
				return await next();
			}

			_timer.Start();
			TResponse response = await next();
			_timer.Stop();

			var name = typeof(TRequest).Name;
			var userExists = _currentUserService.TryGetCurrentUser(out var user);

			if (_optionsSnapshot.Value.WarningLogTimeMilliseconds.HasValue &&
			    _timer.ElapsedMilliseconds > _optionsSnapshot.Value.WarningLogTimeMilliseconds)
			{
				_logger.LogWarning(
					$"{(userExists ? user!.Username : "Anonymous User")} executed request ms {name} ({_timer.ElapsedMilliseconds} milliseconds) which was over longer than {_optionsSnapshot.Value.WarningLogTimeMilliseconds} ms with parameters {_jsonSerializer.Serialize(request)}");
			}
			else if (_optionsSnapshot.Value.LogAllTimes)
			{
				_logger.LogInformation(
					$"{(userExists ? user!.Username : "Anonymous User")} executed request {name} ({_timer.ElapsedMilliseconds} milliseconds) with parameters {_jsonSerializer.Serialize(request)}");
			}

			return response;
		}
	}
}
