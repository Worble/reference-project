using System;
using Forum.Domain.Abstractions.Events;

namespace Forum.Domain.Forum.Threads
{
	public class ThreadCreatedEvent : IDomainEvent
	{
		public ThreadCreatedEvent(Thread thread)
		{
			Thread = thread ?? throw new ArgumentNullException(nameof(thread));
		}

		public Thread Thread { get; }
	}
}
