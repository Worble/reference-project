using System.Collections.Generic;

namespace Forum.Application.Forum.Queries.GetTopic
{
	public class GetTopicViewModel
	{
		public string Name { get; set; }
		public IEnumerable<GetTopicThreadViewModel> Threads { get; set; }
	}
}
