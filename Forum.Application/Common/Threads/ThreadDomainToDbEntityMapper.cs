using System;
using System.Collections.Generic;
using System.Linq;
using Forum.Application.Common.Posts;
using Forum.Application.Common.Topics;
using Forum.Application.Models;
using Forum.Domain.Forum.Posts;
using Forum.Domain.Forum.Threads;
using Mapster;

namespace Forum.Application.Common.Threads
{
	public class
		ThreadDomainToDbEntityMapper : DomainToAuditableDbEntityMapper<Thread,
			ThreadEntity>
	{
		public ThreadDomainToDbEntityMapper()
		{
			ApplyMappingConfiguration();
		}

		public override ThreadEntity MapToDbEntity(Thread? domainEntity)
		{
			if (domainEntity == null)
			{
				throw new ArgumentNullException(nameof(domainEntity));
			}

			return domainEntity.Adapt<ThreadEntity>(Config);
		}

		public override Thread MapToDomainEntity(ThreadEntity? dbEntity)
		{
			if (dbEntity == null)
			{
				throw new ArgumentNullException(nameof(dbEntity));
			}

			return dbEntity.Adapt<Thread>(Config);
		}

		private void ApplyMappingConfiguration()
		{
			Config.NewConfig<Thread, ThreadEntity>()
				.Map(dest => dest.Id, src => src.Id)
				.Map(dest => dest.Posts, src => CreatePostsEntity(src))
				.Map(dest => dest.Title, src => src.Title)
				.Map(dest => dest.Topic, src => CreateTopicEntity(src));

			Config.NewConfig<ThreadEntity, Thread>()
				.Map(dest => dest.Id, src => src.Id)
				.Map(dest => dest.Posts, src => CreatePosts(src))
				.Map(dest => dest.Title, src => src.Title)
				.Map(dest => dest.Topic,
					src => new TopicDomainToDbEntityMapper().MapToDomainEntity(
						src.Topic));
		}

		private static IEnumerable<Post> CreatePosts(ThreadEntity? thread)
		{
			if (thread?.Posts == null)
			{
				throw new ThreadEntityException(
					"Could not create Post Entity from Domain object, see inner exception for details",
					new ArgumentNullException(nameof(thread.Posts)));
			}

			var mapper = new PostDomainToDbEntityMapper();
			return thread.Posts.Select(post => mapper.MapToDomainEntity(post));
		}

		private static IEnumerable<PostEntity> CreatePostsEntity(Thread? thread)
		{
			if (thread?.Posts == null)
			{
				throw new ThreadEntityException(
					"Could not create Post Entity from Domain object, see inner exception for details",
					new ArgumentNullException(nameof(thread.Posts)));
			}

			var mapper = new PostDomainToDbEntityMapper();
			return thread.Posts.Select(post => mapper.MapToDbEntity(post));
		}

		private static TopicEntity CreateTopicEntity(Thread? thread)
		{
			if (thread?.Topic?.Id.HasValue != true)
			{
				throw new ThreadEntityException(
					"Could not create Post Entity from Domain object, see inner exception for details",
					new ArgumentNullException(nameof(thread.Topic)));
			}

			return new TopicEntity {Id = thread.Topic.Id!.Value};
		}
	}
}
