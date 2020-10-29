using System.Collections.Generic;

namespace Forum.Application.Forum.Queries.GetFrontPage
{
	public class GetFrontPageViewModel
	{
		public List<GetFrontPageTopicViewModel> Topics { get; set; } = new List<GetFrontPageTopicViewModel>();
	}
}
