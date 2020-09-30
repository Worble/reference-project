using Forum.Application.Common.Threads;
using Forum.Application.Common.Users;
using Forum.Application.Models;

namespace Forum.Application.Common.Posts
{
	public class PostEntity : AuditableDbEntity
	{
		public string Content { get; set; } = string.Empty;
		public UserEntity? CreatedBy { get; set; }
		public int Id { get; set; }
		public ThreadEntity? Thread { get; set; }
	}
}
