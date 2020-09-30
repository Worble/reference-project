using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Forum.Application.Models.RequestValidationConfigurations;
using MediatR;
using Microsoft.Extensions.Options;

namespace Forum.Application.Behaviours
{
	public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		private readonly IOptionsSnapshot<RequestValidationConfiguration> _optionsSnapshot;
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators,
			IOptionsSnapshot<RequestValidationConfiguration> optionsSnapshot)
		{
			_validators = validators;
			_optionsSnapshot = optionsSnapshot;
		}

		public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
			RequestHandlerDelegate<TResponse> next)
		{
			if (!_optionsSnapshot.Value.Enabled)
			{
				return next();
			}

			var context = new ValidationContext<TRequest>(request);

			var failures = _validators
				.Select(v => v.Validate(context))
				.SelectMany(result => result.Errors)
				.Where(f => f != null)
				.ToList();

			if (failures.Count != 0)
			{
				throw new ValidationException(failures);
			}

			return next();
		}
	}
}
