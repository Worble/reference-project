using System;

namespace Forum.Application.Forum.Queries.GetTopic
{
	public class GetTopicThreadViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public GetTopicUserViewModel CreatedBy { get; set; }
		public DateTime DateCreated { get; set; }
	}
}
