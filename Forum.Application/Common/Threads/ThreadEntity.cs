using System.Collections.Generic;
using Forum.Application.Common.Posts;
using Forum.Application.Common.Topics;
using Forum.Application.Models;

namespace Forum.Application.Common.Threads
{
	public class ThreadEntity : AuditableDbEntity
	{
		public int Id { get; set; }
		public List<PostEntity> Posts { get; internal set; } = new List<PostEntity>();
		public string Title { get; internal set; } = string.Empty;
		public int TopicId { get; set; }
		public TopicEntity? Topic { get; internal set; }
	}
}
