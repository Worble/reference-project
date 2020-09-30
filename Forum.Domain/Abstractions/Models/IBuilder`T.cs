using System.Threading.Tasks;
using Forum.Domain.Models;

namespace Forum.Domain.Abstractions.Models
{
	public interface IBuilder<TEntity> where TEntity : DomainEntity<TEntity>
	{
		Task<TEntity> Build();
	}
}
