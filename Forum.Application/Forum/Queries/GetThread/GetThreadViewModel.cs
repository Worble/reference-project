using System;
using System.Collections.Generic;

namespace Forum.Application.Forum.Queries.GetThread
{
	public class GetThreadViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public GetThreadUserViewModel CreatedBy { get; set; }
		public IEnumerable<GetThreadPostViewModel> Posts { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
