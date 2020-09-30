using Forum.Application.Abstractions.Mapping;
using Forum.Domain.Abstractions.Models;
using Mapster;

namespace Forum.Application.Models
{
	public abstract class
		DomainToAuditableDbEntityMapper<TDomainEntity, TAuditableDbEntity> : IDomainEntityToDbEntityMapper<TDomainEntity
			, TAuditableDbEntity>
		where TDomainEntity : IDomainEntity
		where TAuditableDbEntity : AuditableDbEntity
	{
		protected DomainToAuditableDbEntityMapper()
		{
			ApplyMappingConfiguration();
		}

		internal TypeAdapterConfig Config { get; } = new TypeAdapterConfig();

		public abstract TAuditableDbEntity MapToDbEntity(TDomainEntity domainEntity);

		public abstract TDomainEntity MapToDomainEntity(TAuditableDbEntity dbEntity);

		private void ApplyMappingConfiguration() =>
			// domain -> DB entity
			Config.NewConfig<TDomainEntity, TAuditableDbEntity>()
				.Ignore(e => e.AuditCreatedById!)
				.Ignore(e => e.AuditCreatedByName!)
				.Ignore(e => e.AuditCreatedDateUtc)
				.Ignore(e => e.AuditLastModifiedById!)
				.Ignore(e => e.AuditLastModifiedByName!)
				.Ignore(e => e.AuditLastModifiedUtc!);
	}
}
