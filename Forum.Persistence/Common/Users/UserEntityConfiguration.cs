using Forum.Domain.Forum.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.Common.Users
{
	public class UserEntityConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Password).IsRequired();
			builder.Property(e => e.Username).IsRequired();
			builder.Property(e => e.EmailAddress).IsRequired();
			builder.HasMany(e => e.Posts)
				.WithOne(e => e.CreatedBy!);
			builder.HasMany(e => e.Threads)
				.WithOne(e => e.CreatedBy!);
		}
	}
}
