using System;
using Forum.Domain.Abstractions.Events;

namespace Forum.Domain.Events
{
	public abstract class DomainEvent : IDomainEvent
	{
		public DateTime DateTimeOccurredUtc { get; } = DateTime.UtcNow;
	}
}
