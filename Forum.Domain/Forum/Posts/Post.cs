using System;
using FluentValidation;
using FluentValidation.Results;
using Forum.Domain.Forum.Threads;
using Forum.Domain.Forum.Users;
using Forum.Domain.Models;

namespace Forum.Domain.Forum.Posts
{
	public class Post : DomainEntity<Post>
	{
		public string Content { get; internal set; } = string.Empty;
		public User? CreatedBy { get; internal set; }
		public DateTime CreatedDateUtc { get; internal set; }
		public Thread? Thread { get; internal set; }

		internal override void Validate()
		{
			ValidationResult? validationResults = new PostValidator().Validate(this);
			if (validationResults.IsValid)
			{
				throw new PostException(
					"Thread validation failed, see inner exception for validation errors.",
					new ValidationException(validationResults.Errors));
			}
		}
	}
}
