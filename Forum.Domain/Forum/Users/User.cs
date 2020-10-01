using System;
using FluentValidation;
using FluentValidation.Results;
using Forum.Domain.Models;

namespace Forum.Domain.Forum.Users
{
	public class User : DomainEntity<User>
	{
		public string EmailAddress { get; internal set; } = string.Empty;
		public DateTime JoinDateUtc { get; internal set; }
		public string Password { get; internal set; } = string.Empty;
		public string Username { get; internal set; } = string.Empty;

		internal override void Validate()
		{
			ValidationResult? validationResults = new UserValidator().Validate(this);
			if (!validationResults.IsValid)
			{
				throw new UserException(
					"User validation failed, see inner exception for validation errors.",
					new ValidationException(validationResults.Errors));
			}
		}
	}
}
