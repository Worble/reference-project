using System.Threading;
using System.Threading.Tasks;
using Forum.Application.Common.Forum.AuditEntries;
using Forum.Domain.Forum.Posts;
using Forum.Domain.Forum.Topics;
using Forum.Domain.Forum.Users;
using Microsoft.EntityFrameworkCore;
using Thread = Forum.Domain.Forum.Threads.Thread;

namespace Forum.Application.Abstractions.DbContexts
{
	public interface IForumDbContext
	{
		DbSet<User> Users { get; }
		DbSet<Topic> Topics { get; }
		DbSet<Thread> Threads { get; }
		DbSet<Post> Posts { get; }
		DbSet<AuditEntry> AuditEntries { get; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
