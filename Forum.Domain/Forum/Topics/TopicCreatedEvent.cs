using System;
using Forum.Domain.Abstractions.Events;

namespace Forum.Domain.Forum.Topics
{
	public class TopicCreatedEvent : IDomainEvent
	{
		public TopicCreatedEvent(Topic topic)
		{
			Topic = topic ?? throw new ArgumentNullException(nameof(topic));
		}

		public Topic Topic { get; }
	}
}
