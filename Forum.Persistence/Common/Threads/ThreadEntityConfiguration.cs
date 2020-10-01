using Forum.Application.Common.Threads;
using Forum.Persistence.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.Common.Threads
{
	public class ThreadEntityConfiguration : BaseAuditableDbEntityConfiguration<ThreadEntity>
	{
		public override void Configure(EntityTypeBuilder<ThreadEntity> builder)
		{
			base.Configure(builder);
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Title).IsRequired();
			builder.HasMany(e => e.Posts).WithOne(e => e.Thread!);
			builder.HasOne(e => e.Topic)
				.WithMany(e => e!.Threads)
				.HasForeignKey(e => e.TopicId)
				.IsRequired();
		}
	}
}
