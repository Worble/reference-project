using System.Collections.Generic;
using FluentValidation;
using Forum.Domain.Forum.SubForums;
using Forum.Domain.Forum.Threads;
using Forum.Domain.Models;

namespace Forum.Domain.Forum.Topics
{
	public class Topic : DomainEntity<Topic>
	{
		internal Topic()
		{
		}

		public string Title { get; internal set; } = string.Empty;

		public int? ParentId { get; internal set; }
		public Topic? Parent { get; internal set; }
		public int SubForumId { get; internal set; }
		public SubForum? SubForum { get; internal set; }

		public List<Topic> Children { get; internal set; } = new List<Topic>();
		public List<Thread> Threads { get; internal set; } = new List<Thread>();

		internal override void Validate()
		{
			var validationResults = new TopicValidator().Validate(this);
			if (!validationResults.IsValid)
			{
				throw new TopicException(
					"Topic validation failed, see inner exception for validation errors.",
					new ValidationException(validationResults.Errors));
			}
		}
	}
}
