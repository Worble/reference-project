using Forum.Application.Common.Forum.AuditEntries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Infrastructure.Common.AuditEntries
{
	public class AuditEntryEntityConfiguration : IEntityTypeConfiguration<AuditEntry>
	{
		public void Configure(EntityTypeBuilder<AuditEntry> builder)
		{
			builder.HasKey(e => e.Id);
			builder.Property(e => e.CreateDate).IsRequired();
			builder.Property(e => e.CreatedById).IsRequired();
			builder.Property(e => e.CreatedByName).IsRequired();
		}
	}
}
