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
				.Map(dest => dest.CreatedBy, src => CreateUserEntity(src))
				.Map(dest => dest.Id, src => src.Id)
				.Map(dest => dest.Thread, src => CreateThreadEntity(src));

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

		private static ThreadEntity CreateThreadEntity(Post? post)
		{
			if (post?.Thread?.Id.HasValue != true)
			{
				throw new PostEntityException(
					"Could not create Post Entity from Domain object, see inner exception for details",
					new ArgumentNullException(nameof(post.Thread)));
			}

			return new ThreadEntity {Id = post.Thread.Id!.Value};
		}

		private static UserEntity CreateUserEntity(Post? post)
		{
			if (post?.CreatedBy?.Id.HasValue != true)
			{
				throw new PostEntityException(
					"Could not create Post Entity from Domain object, see inner exception for details",
					new ArgumentNullException(nameof(post.CreatedBy)));
			}

			return new UserEntity {Id = post.CreatedBy.Id!.Value};
		}
	}
}
