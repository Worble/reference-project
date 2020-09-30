using System.Collections.Generic;
using Forum.Application.Common.Posts;
using Forum.Application.Models;

namespace Forum.Application.Common.Users
{
	public class UserEntity : AuditableDbEntity
	{
		public string EmailAddress { get; internal set; } = string.Empty;
		public int Id { get; set; }
		public string Password { get; internal set; } = string.Empty;
		public string Username { get; internal set; } = string.Empty;
		public List<PostEntity> Posts { get; set; } = new List<PostEntity>();
	}
}
