using Forum.Application.Abstractions.Models;
using Forum.Domain.Abstractions.Models;

namespace Forum.Application.Abstractions.Mapping
{
	public interface IDomainEntityToDbEntityMapper<TDomainEntity, TDbEntity>
		where TDomainEntity : IDomainEntity where TDbEntity : IDbEntity
	{
		TDbEntity MapToDbEntity(TDomainEntity domainEntity);

		TDomainEntity MapToDomainEntity(TDbEntity dbEntity);
	}
}
