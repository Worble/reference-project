using System.Collections.Generic;

namespace Forum.Application.Forum.Queries.GetFrontPage
{
	public class GetFrontPageTopicViewModel
	{
		public string Title { get; set; } = string.Empty;
		public IEnumerable<GetFrontPageTopicViewModel> Children { get; set; } = new List<GetFrontPageTopicViewModel>();
	}
}
