using Forum.Domain.Abstractions.Models;

namespace Forum.Domain.Models
{
	public abstract class DomainEntity<TEntity> : DomainEntity where TEntity : class, IDomainEntity
	{
		internal abstract void Validate();

		protected TEntity CreateShallowCopy() => (TEntity)MemberwiseClone();
	}
}
