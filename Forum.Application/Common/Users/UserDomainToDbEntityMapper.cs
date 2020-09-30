using System;
using Forum.Application.Models;
using Forum.Domain.Forum.Users;
using Mapster;

namespace Forum.Application.Common.Users
{
	public class UserDomainToDbEntityMapper : DomainToAuditableDbEntityMapper<User, UserEntity>
	{
		public UserDomainToDbEntityMapper()
		{
			ApplyMappingConfiguration();
		}

		public override UserEntity MapToDbEntity(User? domainEntity)
		{
			if (domainEntity == null)
			{
				throw new ArgumentNullException(nameof(domainEntity));
			}

			return domainEntity.Adapt<UserEntity>(Config);
		}

		public override User MapToDomainEntity(UserEntity? dbEntity)
		{
			if (dbEntity == null)
			{
				throw new ArgumentNullException(nameof(dbEntity));
			}

			return dbEntity.Adapt<User>(Config);
		}

		private void ApplyMappingConfiguration()
		{
			// domain -> DB entity
			_ = Config.NewConfig<User, UserEntity>()
				.Map(dest => dest.EmailAddress, src => src.EmailAddress)
				.Map(dest => dest.AuditCreatedDateUtc, src => src.JoinDateUtc)
				.Map(dest => dest.Password, src => src.Password)
				.Map(dest => dest.Username, src => src.Username)
				.Map(dest => dest.Id, src => src.Id);

			// DB entity -> domain
			_ = Config.NewConfig<UserEntity, User>()
				.Map(dest => dest.EmailAddress, src => src.EmailAddress)
				.Map(dest => dest.Id, src => src.Id)
				.Map(dest => dest.JoinDateUtc, src => src.AuditCreatedDateUtc)
				.Map(dest => dest.Password, src => src.Password)
				.Map(dest => dest.Username, src => src.Username);
		}
	}
}
