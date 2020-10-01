using System;
using System.Linq;
using Forum.Application.Common.Posts;
using Forum.Application.Common.Topics;
using Forum.Application.Models;
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
				.Map(dest => dest.Posts, src => src.Posts.Select(
					post => new PostDomainToDbEntityMapper().MapToDbEntity(post)
				))
				.Map(dest => dest.Title, src => src.Title)
				.Map(dest => dest.Topic, src => src.Topic != null && !src.Topic.Id.HasValue
					? new TopicDomainToDbEntityMapper().MapToDbEntity(src.Topic)
					: null)
				.Map(dest => dest.TopicId, src => src.Topic != null
					? src.Topic.Id ?? default
					: default);

			Config.NewConfig<ThreadEntity, Thread>()
				.Map(dest => dest.Id, src => src.Id)
				.Map(dest => dest.Posts, src => src.Posts.Select(
					post => new PostDomainToDbEntityMapper().MapToDomainEntity(post)
				))
				.Map(dest => dest.Title, src => src.Title)
				.Map(dest => dest.Topic, src => src.Topic != null
					? new TopicDomainToDbEntityMapper().MapToDomainEntity(src.Topic)
					: null);
		}
	}
}
