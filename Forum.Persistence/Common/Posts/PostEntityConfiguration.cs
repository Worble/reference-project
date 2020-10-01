using Forum.Application.Common.Posts;
using Forum.Persistence.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.Common.Posts
{
	public class PostEntityConfiguration : BaseAuditableDbEntityConfiguration<PostEntity>
	{
		public override void Configure(EntityTypeBuilder<PostEntity> builder)
		{
			base.Configure(builder);
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Content).IsRequired();
			builder
				.HasOne(e => e.CreatedBy)
				.WithMany(e => e!.Posts)
				.HasForeignKey(e => e.CreatedById)
				.IsRequired();
			builder
				.HasOne(e => e.Thread)
				.WithMany(e => e!.Posts)
				.HasForeignKey(e => e.ThreadId)
				.IsRequired();
		}
	}
}
