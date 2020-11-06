using System;
using Forum.Domain.Events;

namespace Forum.Domain.Forum.Users
{
	public class UserRegisteredEvent : DomainEvent
	{
		public UserRegisteredEvent(User user)
		{
			User = user ?? throw new ArgumentNullException(nameof(user));
		}

		public User User { get; }
	}
}
