using System.Collections.Generic;
using FluentValidation;
using Forum.Domain.Forum.Topics;
using Forum.Domain.Models;

namespace Forum.Domain.Forum.SubForums
{
	public class SubForum : DomainEntity<SubForum>
	{
		internal SubForum()
		{
		}

		public string Title { get; set; } = string.Empty;
		public List<Topic> Topics { get; internal set; } = new List<Topic>();

		internal override void Validate()
		{
			var validationResults = new SubForumValidator().Validate(this);
			if (!validationResults.IsValid)
			{
				throw new TopicException(
					"Sub Forum validation failed, see inner exception for validation errors.",
					new ValidationException(validationResults.Errors));
			}
		}
	}
}
