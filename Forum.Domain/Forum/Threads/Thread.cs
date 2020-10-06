using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using Forum.Domain.Forum.Posts;
using Forum.Domain.Forum.Topics;
using Forum.Domain.Forum.Users;
using Forum.Domain.Models;

namespace Forum.Domain.Forum.Threads
{
	public class Thread : DomainEntity<Thread>
	{
		public List<Post> Posts { get; internal set; } = new List<Post>();
		public string Title { get; internal set; } = string.Empty;

		public DateTime CreatedDate { get; internal set; } = DateTime.Now;
		public Topic? Topic { get; internal set; }

		public User? CreatedBy { get; internal set; }

		internal override void Validate()
		{
			ValidationResult? validationResults = new ThreadValidator().Validate(this);
			if (!validationResults.IsValid)
			{
				throw new ThreadException(
					"Thread validation failed, see inner exception for validation errors.",
					new ValidationException(validationResults.Errors));
			}
		}
	}
}
