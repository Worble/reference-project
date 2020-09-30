using Forum.Application.Common.Users;
using Forum.Persistence.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.Common.Users
{
	public class UserEntityConfiguration : BaseAuditableDbEntityConfiguration<UserEntity>
	{
		public override void Configure(EntityTypeBuilder<UserEntity> builder)
		{
			base.Configure(builder);
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Password).IsRequired();
			builder.Property(e => e.Username).IsRequired();
			builder.Property(e => e.EmailAddress).IsRequired();
			builder.HasMany(e => e.Posts).WithOne(e => e.CreatedBy!);
		}
	}
}
