using Forum.Domain.Forum.SubForums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.Common.SubForums
{
	public class SubForumEntityConfiguration : IEntityTypeConfiguration<SubForum>
	{
		public void Configure(EntityTypeBuilder<SubForum> builder)
		{
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Title).IsRequired();
			builder.HasIndex(e => e.Title).IsUnique();
		}
	}
}
