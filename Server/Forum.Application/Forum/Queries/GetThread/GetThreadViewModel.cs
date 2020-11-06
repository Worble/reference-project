using System;
using System.Collections.Generic;

namespace Forum.Application.Forum.Queries.GetThread
{
	public class GetThreadViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public GetThreadUserViewModel? CreatedBy { get; set; }
		public IEnumerable<GetThreadPostViewModel> Posts { get; set; } = new List<GetThreadPostViewModel>();
		public DateTime CreatedDate { get; set; }
	}
}
