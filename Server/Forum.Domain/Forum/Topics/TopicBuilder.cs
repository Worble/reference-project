using System;
using Forum.Domain.Abstractions.Models;
using Forum.Domain.Forum.SubForums;

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
			_topic.Title = name ?? throw new ArgumentNullException(nameof(name));
			return this;
		}

		public TopicBuilder WithParent(int parentId)
		{
			_topic.ParentId = parentId;
			return this;
		}

		public TopicBuilder WithParent(Topic parent)
		{
			_topic.Parent = parent ?? throw new ArgumentNullException(nameof(parent));
			return this;
		}

		public TopicBuilder UnderSubForum(SubForum subForum)
		{
			_topic.SubForum = subForum ?? throw new ArgumentNullException(nameof(subForum));
			return this;
		}

		public TopicBuilder UnderSubForum(int subForumId)
		{
			_topic.SubForumId = subForumId;
			return this;
		}
	}
}
