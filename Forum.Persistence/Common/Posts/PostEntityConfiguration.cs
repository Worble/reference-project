using Forum.Domain.Forum.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.Common.Posts
{
	public class PostEntityConfiguration : IEntityTypeConfiguration<Post>
	{
		public void Configure(EntityTypeBuilder<Post> builder)
		{
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Content).IsRequired();
			builder
				.HasOne(e => e.CreatedBy)
				.WithMany(e => e!.Posts)
				.IsRequired();
			builder
				.HasOne(e => e.Thread)
				.WithMany(e => e!.Posts)
				.IsRequired();
		}
	}
}
