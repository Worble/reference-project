using System;
using Forum.Domain.Abstractions.Models;

namespace Forum.Domain.Forum.Topics
{
	public class TopicBuilder : IBuilder<Topic>
	{
		private readonly Topic _topic;

		public TopicBuilder()
		{
			_topic = new Topic();
		}

		public Topic Build()
		{
			_topic.Validate();
			_topic.AddDomainEvent(new TopicCreatedEvent(_topic));
			return _topic;
		}

		public TopicBuilder WithName(string name)
		{
			_topic.Name = name ?? throw new ArgumentNullException(nameof(name));
			return this;
		}

		public TopicBuilder WithParent(Topic parent)
		{
			_topic.Parent = parent ?? throw new ArgumentNullException(nameof(parent));
			return this;
		}
	}
}
