using System.Threading;
using System.Threading.Tasks;
using Forum.Application.Abstractions.Dates;
using Forum.Application.Abstractions.DbContexts;
using Forum.Application.Abstractions.Identity;
using Forum.Application.Common.Forum.AuditEntries;
using Forum.Application.Models;
using Forum.Domain.Forum.Posts;
using Forum.Domain.Forum.Topics;
using Forum.Domain.Forum.Users;
using Forum.Persistence.Models.ForumDbContextConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Thread = Forum.Domain.Forum.Threads.Thread;

namespace Forum.Persistence.DbContexts
{
	public class ForumDbContext : DbContext, IForumDbContext
	{
		private readonly ICurrentUserService _currentUserService;
		private readonly IDateTimeService _dateTimeService;
		private readonly IOptionsSnapshot<ForumDbContextConfiguration> _options;

		public ForumDbContext(
			DbContextOptions<ForumDbContext> options,
			ICurrentUserService currentUserService, IOptionsSnapshot<ForumDbContextConfiguration> optionsSnapshot,
			IDateTimeService dateTimeService)
			: base(options)
		{
			_currentUserService = currentUserService;
			_options = optionsSnapshot;
			_dateTimeService = dateTimeService;
		}

		public DbSet<Post> Posts { get; } = default!;
		public DbSet<Thread> Threads { get; } = default!;
		public DbSet<Topic> Topics { get; } = default!;
		public DbSet<User> Users { get; } = default!;
		public DbSet<AuditEntry> AuditEntries { get; } = default!;

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			foreach (var entry in ChangeTracker.Entries<AuditableDbEntity>())
			{
				var userExists = _currentUserService.TryGetCurrentUser(out var user);
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.AuditCreatedById = userExists
							? user!.Id.ToString()
							: _options.Value.DefaultUserId;

						entry.Entity.AuditCreatedByName = userExists
							? user!.Username
							: _options.Value.DefaultUsername;

						entry.Entity.AuditCreatedDateUtc = _dateTimeService.UtcNow;
						break;

					case EntityState.Modified:
						entry.Entity.AuditLastModifiedById = userExists
							? user!.Id.ToString()
							: _options.Value.DefaultUserId;

						entry.Entity.AuditLastModifiedByName = userExists
							? user!.Username
							: _options.Value.DefaultUsername;

						entry.Entity.AuditLastModifiedUtc = _dateTimeService.UtcNow;
						break;
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) =>
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ForumDbContext).Assembly);
	}
}
