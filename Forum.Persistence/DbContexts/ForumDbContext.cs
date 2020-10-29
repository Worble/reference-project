using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Audit.EntityFramework;
using Forum.Application.Abstractions.DbContexts;
using Forum.Application.Common.Audit;
using Forum.Domain.Forum.Posts;
using Forum.Domain.Forum.Topics;
using Forum.Domain.Forum.Users;
using Forum.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Thread = Forum.Domain.Forum.Threads.Thread;

namespace Forum.Persistence.DbContexts
{
	public class ForumDbContext : AuditDbContext, IForumDbContext
	{
		public ForumDbContext(DbContextOptions<ForumDbContext> options)
			: base(options)
		{
		}

		public DbSet<Post> Posts { get; set; }
		public DbSet<AuditLog> AuditLogs { get; set; }
		public DbSet<Thread> Threads { get; set; }
		public DbSet<Topic> Topics { get; set; }
		public DbSet<User> Users { get; set; }

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			var result = await base.SaveChangesAsync(cancellationToken);

			var tasks = ChangeTracker.Entries<DomainEntity>().Select(entry => entry.Entity.DispatchDomainEventsAsync());
			await Task.WhenAll(tasks);

			return result;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) =>
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ForumDbContext).Assembly);
	}
}
