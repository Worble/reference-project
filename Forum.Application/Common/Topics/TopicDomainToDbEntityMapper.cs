using System;
using System.Collections.Generic;
using System.Linq;
using Forum.Application.Common.Threads;
using Forum.Application.Models;
using Forum.Domain.Forum.Threads;
using Forum.Domain.Forum.Topics;
using Mapster;

namespace Forum.Application.Common.Topics
{
	public class TopicDomainToDbEntityMapper : DomainToAuditableDbEntityMapper<Topic, TopicEntity>
	{
		public TopicDomainToDbEntityMapper()
		{
			ApplyMappingConfiguration();
		}

		public override TopicEntity MapToDbEntity(Topic? domainEntity)
		{
			if (domainEntity == null)
			{
				throw new ArgumentNullException(nameof(domainEntity));
			}

			return domainEntity.Adapt<TopicEntity>(Config);
		}

		public override Topic MapToDomainEntity(TopicEntity? dbEntity)
		{
			if (dbEntity == null)
			{
				throw new ArgumentNullException(nameof(dbEntity));
			}

			return dbEntity.Adapt<Topic>(Config);
		}

		private void ApplyMappingConfiguration()
		{
			Config.NewConfig<Topic, TopicEntity>()
				.Map(dest => dest.Id, src => src.Id)
				.Map(dest => dest.Name, src => src.Name)
				.Map(dest => dest.Parent, src => src.Parent != null && !src.Parent.Id.HasValue
					? MapToDbEntity(src.Parent)
					: null)
				.Map(dest => dest.ParentId, src => src.Parent != null
					? src.Parent.Id ?? default
					: default)
				.Ignore(dest => dest.Threads)
				.Ignore(dest => dest.Children);

			Config.NewConfig<TopicEntity, Topic>()
				.Map(dest => dest.Id, src => src.Id)
				.Map(dest => dest.Name, src => src.Name)
				.Map(dest => dest.Threads, src => CreateThreads(src))
				.Map(dest => dest.Parent, src => src.Parent != null ? MapToDomainEntity(src.Parent) : null)
				.Map(dest => dest.Children, src => src.Children.Select(topic => MapToDomainEntity(topic)));
		}

		private static IEnumerable<Thread> CreateThreads(TopicEntity? topic)
		{
			if (topic?.Threads == null)
			{
				throw new TopicEntityException(
					"Could not create Post Entity from Domain object, see inner exception for details",
					new ArgumentNullException(nameof(topic.Threads)));
			}

			var mapper = new ThreadDomainToDbEntityMapper();
			return topic.Threads.Select(thread => mapper.MapToDomainEntity(thread));
		}
	}
}
