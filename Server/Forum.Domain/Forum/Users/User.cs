using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using FluentValidation;
using Forum.Common.JsonConverters;
using Forum.Domain.Forum.Posts;
using Forum.Domain.Forum.Threads;
using Forum.Domain.Models;

namespace Forum.Domain.Forum.Users
{
	public class User : DomainEntity<User>
	{
		internal User()
		{
		}

		public string EmailAddress { get; internal set; } = string.Empty;
		public DateTime JoinDateUtc { get; internal set; }

		[JsonConverter(typeof(SensitiveInformationConverter))]
		public string Password { get; internal set; } = string.Empty;

		public string Username { get; internal set; } = string.Empty;
		public ICollection<Post> Posts { get; } = default!;
		public ICollection<Thread> Threads { get; } = default!;

		internal override void Validate()
		{
			var validationResults = new UserValidator().Validate(this);
			if (!validationResults.IsValid)
			{
				throw new UserException(
					"User validation failed, see inner exception for validation errors.",
					new ValidationException(validationResults.Errors));
			}
		}
	}
}
