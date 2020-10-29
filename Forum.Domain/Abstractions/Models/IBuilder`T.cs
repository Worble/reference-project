using Forum.Domain.Models;

namespace Forum.Domain.Abstractions.Models
{
	public interface IBuilder<out TEntity> where TEntity : DomainEntity<TEntity>
	{
		TEntity Build();
	}
}
