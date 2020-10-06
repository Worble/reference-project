using Forum.Domain.Forum.Topics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.Common.Topics
{
	public class TopicEntityConfiguration : IEntityTypeConfiguration<Topic>
	{
		public void Configure(EntityTypeBuilder<Topic> builder)
		{
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Name).IsRequired();
			builder.HasMany(e => e.Threads)
				.WithOne(e => e.Topic!);
			builder.HasOne(e => e.Parent)
				.WithMany(e => e!.Children);
		}
	}
}
