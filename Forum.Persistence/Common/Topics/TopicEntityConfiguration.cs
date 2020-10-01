using Forum.Application.Common.Topics;
using Forum.Persistence.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.Common.Topics
{
	public class TopicEntityConfiguration : BaseAuditableDbEntityConfiguration<TopicEntity>
	{
		public override void Configure(EntityTypeBuilder<TopicEntity> builder)
		{
			base.Configure(builder);
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Name).IsRequired();
			builder.HasMany(e => e.Threads).WithOne(e => e.Topic!);
			builder.HasOne(e => e.Parent)
				.WithMany(e => e.Children!)
				.HasForeignKey(e => e.ParentId);
		}
	}
}
