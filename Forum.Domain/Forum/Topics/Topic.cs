using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using Forum.Domain.Forum.Threads;
using Forum.Domain.Models;

namespace Forum.Domain.Forum.Topics
{
	public class Topic : DomainEntity<Topic>
	{
		internal Topic()
		{
		}

		public string Name { get; internal set; } = string.Empty;

		public Topic? Parent { get; internal set; }

		public List<Topic> Children { get; internal set; } = new List<Topic>();
		public List<Thread> Threads { get; internal set; } = new List<Thread>();

		internal override void Validate()
		{
			ValidationResult? validationResults = new TopicValidator().Validate(this);
			if (validationResults.IsValid)
			{
				throw new TopicException(
					"Topic validation failed, see inner exception for validation errors.",
					new ValidationException(validationResults.Errors));
			}
		}
	}
}
