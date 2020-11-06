using System;
using System.Collections.Generic;
using FluentValidation;
using Forum.Domain.Forum.Posts;
using Forum.Domain.Forum.Topics;
using Forum.Domain.Forum.Users;
using Forum.Domain.Models;

namespace Forum.Domain.Forum.Threads
{
	public class Thread : DomainEntity<Thread>
	{
		internal Thread()
		{
		}

		public string Title { get; internal set; } = string.Empty;

		public DateTime CreatedDate { get; internal set; }
		public int TopicId { get; internal set; }
		public Topic? Topic { get; internal set; }
		public int CreatedById { get; internal set; }
		public User? CreatedBy { get; internal set; }

		public List<Post> Posts { get; internal set; } = new List<Post>();

		internal override void Validate()
		{
			var validationResults = new ThreadValidator().Validate(this);
			if (!validationResults.IsValid)
			{
				throw new ThreadException(
					"Thread validation failed, see inner exception for validation errors.",
					new ValidationException(validationResults.Errors));
			}
		}
	}
}
