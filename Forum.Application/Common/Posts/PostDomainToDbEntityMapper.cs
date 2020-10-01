using System;
using Forum.Application.Common.Threads;
using Forum.Application.Common.Users;
using Forum.Application.Models;
using Forum.Domain.Forum.Posts;
using Mapster;

namespace Forum.Application.Common.Posts
{
	public class
		PostDomainToDbEntityMapper : DomainToAuditableDbEntityMapper<Post,
			PostEntity>
	{
		public PostDomainToDbEntityMapper()
		{
			ApplyMappingConfiguration();
		}

		public override PostEntity MapToDbEntity(Post? domainEntity)
		{
			if (domainEntity == null)
			{
				throw new ArgumentNullException(nameof(domainEntity));
			}

			return domainEntity.Adapt<PostEntity>(Config);
		}

		public override Post MapToDomainEntity(PostEntity? dbEntity)
		{
			if (dbEntity == null)
			{
				throw new ArgumentNullException(nameof(dbEntity));
			}

			return dbEntity.Adapt<Post>(Config);
		}

		private void ApplyMappingConfiguration()
		{
			// domain -> DB entity
			Config.NewConfig<Post, PostEntity>()
				.Map(dest => dest.Content, src => src.Content)
				.Map(dest => dest.AuditCreatedDateUtc, src => src.CreatedDateUtc)
				.Map(dest => dest, src => src.Content)
				.Map(dest => dest.Content, src => src.Content)
				.Map(dest => dest.Id, src => src.Id)
				.Map(dest => dest.CreatedBy, src => src.CreatedBy != null && !src.CreatedBy.Id.HasValue
					? new UserDomainToDbEntityMapper().MapToDbEntity(
						src.CreatedBy)
					: null)
				.Map(dest => dest.CreatedById, src => src.CreatedBy != null
					? src.CreatedBy.Id ?? default
					: default)
				.Map(dest => dest.Thread, src => src.Thread != null && !src.Thread.Id.HasValue
					? new ThreadDomainToDbEntityMapper().MapToDbEntity(
						src.Thread)
					: null)
				.Map(dest => dest.ThreadId, src => src.Thread != null
					? src.Thread.Id ?? default
					: default)
				;

			// DB entity -> domain
			Config.NewConfig<PostEntity, Post>()
				.Map(dest => dest.Content, src => src.Content)
				.Map(dest => dest.CreatedDateUtc, src => src.AuditCreatedDateUtc)
				.Map(dest => dest.CreatedBy, src => src.CreatedBy != null
					? new UserDomainToDbEntityMapper().MapToDomainEntity(
						src.CreatedBy)
					: null)
				.Map(dest => dest.CreatedDateUtc, src => src.AuditCreatedDateUtc)
				.Map(dest => dest.Id, src => src.Id)
				.Map(dest => dest.Thread, src => src.Thread != null
					? new ThreadDomainToDbEntityMapper().MapToDomainEntity(
						src.Thread)
					: null);
		}
	}
}
