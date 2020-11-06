using Forum.Application.Common.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.Common.AuditLogs
{
	public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
	{
		public void Configure(EntityTypeBuilder<AuditLog> builder)
		{
			builder.HasKey(e => e.Id);
			builder.Property(e => e.AuditData).HasColumnType("jsonb");
		}
	}
}
