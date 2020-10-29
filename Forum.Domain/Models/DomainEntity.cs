using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.Domain.Abstractions.Events;
using Forum.Domain.Abstractions.Models;
using Forum.Domain.Events;

namespace Forum.Domain.Models
{
	public abstract class DomainEntity : IDomainEntity
	{
		private static IDomainEventDispatcher _dispatcher = new NullDomainEventDispatcher();

		private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

		public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

		public int Id { get; } = default;

		public void AddDomainEvent(IDomainEvent eventItem) => _domainEvents.Add(eventItem);

		public void ClearDomainEvents() => _domainEvents.Clear();

		public async Task DispatchDomainEventsAsync()
		{
			foreach (var domainEvent in _domainEvents)
			{
				await _dispatcher
					.PublishAsync(domainEvent)
					.ConfigureAwait(false);
			}

			ClearDomainEvents();
		}

		public bool Equals(DomainEntity? other) => other != null && Id.Equals(other.Id);

		public override bool Equals(object? other) => other is DomainEntity entity
			? Equals(entity)
			: base.Equals(other);

		public override int GetHashCode() => Id != default ? Id.GetHashCode() : 0;

		public bool IsTransient() => Id != default;

		public void RemoveDomainEvent(IDomainEvent eventItem) => _domainEvents.Remove(eventItem);

		internal static void WireUpDispatcher(IDomainEventDispatcher dispatcher) => _dispatcher = dispatcher;
	}
}
