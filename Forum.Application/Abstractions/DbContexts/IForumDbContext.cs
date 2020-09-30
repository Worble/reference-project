using System.Threading;
using System.Threading.Tasks;
using Forum.Application.Common.Posts;
using Forum.Application.Common.Threads;
using Forum.Application.Common.Topics;
using Forum.Application.Common.Users;
using Microsoft.EntityFrameworkCore;

namespace Forum.Application.Abstractions.DbContexts
{
	public interface IForumDbContext
	{
		DbSet<UserEntity> Users { get; }
		DbSet<TopicEntity> Topics { get; }
		DbSet<ThreadEntity> Threads { get; }
		DbSet<PostEntity> Posts { get; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
