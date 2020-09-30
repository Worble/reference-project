using System;
using System.Threading.Tasks;
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

		public async Task<Topic> Build()
		{
			_topic.Validate();
			_topic.AddDomainEvent(new TopicCreatedEvent(_topic));
			await _topic
				.DispatchDomainEventsAsync()
				.ConfigureAwait(false);
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
