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
			builder.Property(e => e.Title).IsRequired();
			builder
				.HasOne(e => e.Parent)
				.WithMany(e => e!.Children)
				.HasForeignKey(e => e.ParentId);
			builder
				.HasOne(e => e.SubForum)
				.WithMany(e => e!.Topics)
				.HasForeignKey(e => e.SubForumId)
				.IsRequired();
		}
	}
}
