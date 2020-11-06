using System.Collections.Generic;

namespace Forum.Application.Forum.Queries.GetAllSubForums
{
	public class GetAllSubForumsViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;

		public IEnumerable<GetAllSubForumsTopicViewModel> Topics { get; set; } =
			new List<GetAllSubForumsTopicViewModel>();	}
}
