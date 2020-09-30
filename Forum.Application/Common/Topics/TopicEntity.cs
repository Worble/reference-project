using System.Collections.Generic;
using Forum.Application.Common.Threads;
using Forum.Application.Models;

namespace Forum.Application.Common.Topics
{
	public class TopicEntity : AuditableDbEntity
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public TopicEntity? Parent { get; set; }

		public List<TopicEntity> Children { get; set; } = new List<TopicEntity>();
		public List<ThreadEntity> Threads { get; set; } = new List<ThreadEntity>();
	}
}
