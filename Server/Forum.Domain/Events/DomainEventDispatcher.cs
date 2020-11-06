using System;
using System.Threading.Tasks;
using Forum.Domain.Abstractions.Events;
using MediatR;

namespace Forum.Domain.Events
{
	public class DomainEventDispatcher : IDomainEventDispatcher
	{
		private readonly IMediator _mediator;

		public DomainEventDispatcher(IMediator mediator)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		public Task PublishAsync<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent =>
			_mediator.Publish(domainEvent);
	}
}
