using System.Collections.Generic;

namespace Forum.Application.Forum.Queries.GetTopic
{
	public class GetTopicViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public IEnumerable<GetTopicThreadViewModel> Threads { get; set; } = new List<GetTopicThreadViewModel>();
		public GetTopicViewModel? Parent { get; set; }
		public IEnumerable<GetTopicViewModel> Children { get; set; } = new List<GetTopicViewModel>();
	}
}
