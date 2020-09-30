using System.Threading.Tasks;

namespace Forum.Domain.Abstractions.Events
{
	internal interface IDomainEventDispatcher
	{
		Task PublishAsync<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent;
	}
}
