using Forum.Domain.Abstractions.Events;

namespace Forum.Domain.Forum.Posts
{
	internal class PostCreatedEvent : IDomainEvent
	{
		public PostCreatedEvent(Post post)
		{
			Post = post;
		}

		public Post Post { get; }
	}
}
