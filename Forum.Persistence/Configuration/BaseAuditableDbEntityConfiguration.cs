using Forum.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.Configuration
{
	public abstract class BaseAuditableDbEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
		where TEntity : AuditableDbEntity
	{
		public virtual void Configure(EntityTypeBuilder<TEntity> builder)
		{
			builder.Property(e => e.AuditCreatedById).IsRequired();
			builder.Property(e => e.AuditCreatedByName).IsRequired();
			builder.Property(e => e.AuditCreatedDateUtc).IsRequired();
		}
	}
}
