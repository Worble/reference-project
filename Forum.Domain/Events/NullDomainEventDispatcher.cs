using System.Threading.Tasks;
using Forum.Domain.Abstractions.Events;

namespace Forum.Domain.Events
{
	public sealed class NullDomainEventDispatcher : IDomainEventDispatcher
	{
		public Task PublishAsync<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent => Task.CompletedTask;
	}
}
