using System;
using Forum.Domain.Abstractions.Events;

namespace Forum.Domain.Forum.SubForums
{
	public class SubForumCreatedEvent : IDomainEvent
	{
		public SubForumCreatedEvent(SubForum subForum)
		{
			SubForum = subForum ?? throw new ArgumentNullException(nameof(subForum));
		}

		public SubForum SubForum { get; }
	}
}
