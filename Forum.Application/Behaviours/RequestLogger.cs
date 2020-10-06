using System.Threading;
using System.Threading.Tasks;
using Forum.Application.Abstractions.Identity;
using Forum.Application.Abstractions.Serialization;
using Forum.Application.Models.RequestLoggingConfigurations;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Forum.Application.Behaviours
{
	public class RequestLogger<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : notnull
	{
		private readonly ICurrentUserService _currentUserService;
		private readonly IJsonSerializer _jsonSerializer;
		private readonly ILogger<TRequest> _logger;
		private readonly IOptionsSnapshot<RequestLoggingConfiguration> _optionsSnapshot;

		public RequestLogger(ILogger<TRequest> logger, IOptionsSnapshot<RequestLoggingConfiguration> optionsSnapshot,
			ICurrentUserService currentUserService,
			IJsonSerializer jsonSerializer)
		{
			_logger = logger;
			_optionsSnapshot = optionsSnapshot;
			_currentUserService = currentUserService;
			_jsonSerializer = jsonSerializer;
		}

		public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
			RequestHandlerDelegate<TResponse> next)
		{
			if (!_optionsSnapshot.Value.Enabled)
			{
				return next();
			}

			var requestName = typeof(TRequest).Name;
			_currentUserService.TryGetCurrentUser(out var user);

			_logger.LogInformation(
				$"User {(user != null ? user.Username : string.Empty)} making request {requestName} with parameters {_jsonSerializer.Serialize(request)}");
			return next();
		}
	}
}
