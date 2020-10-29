using System;

namespace Forum.Application.Forum.Queries.GetThread
{
	public class GetThreadPostViewModel
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public DateTime CreatedDate { get; set; }
		public GetThreadUserViewModel CreatedBy { get; set; }
	}
}
