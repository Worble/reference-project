using Forum.Domain.Forum.Threads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.Common.Threads
{
	public class ThreadEntityConfiguration : IEntityTypeConfiguration<Thread>
	{
		public void Configure(EntityTypeBuilder<Thread> builder)
		{
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Title).IsRequired();
			builder.Property(e => e.CreatedDate).IsRequired();
			builder.HasMany(e => e.Posts).WithOne(e => e.Thread!);
			builder
				.HasOne(e => e.Topic)
				.WithMany(e => e!.Threads)
				.HasForeignKey(e => e.TopicId)
				.IsRequired();
			builder
				.HasOne(e => e.CreatedBy)
				.WithMany(e => e!.Threads)
				.HasForeignKey(e => e.CreatedById)
				.IsRequired();
		}
	}
}
