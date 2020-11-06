using System.Threading;
using System.Threading.Tasks;
using Forum.Application.Abstractions.Dates;
using Forum.Application.Abstractions.DbContexts;
using Forum.Domain.Forum.Posts;
using Forum.Domain.Forum.SubForums;
using Forum.Domain.Forum.Threads;
using Forum.Domain.Forum.Topics;
using Forum.Domain.Forum.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Application.Forum.Commands.Seed
{
	public class SeedCommand : IRequest
	{}

	public class SeedCommandHandler : IRequestHandler<SeedCommand>
	{
		private readonly IDateTimeService _dateTimeService;
		private readonly IForumDbContext _dbContext;

		public SeedCommandHandler(IForumDbContext dbContext,
			IDateTimeService dateTimeService)
		{
			_dbContext = dbContext;
			_dateTimeService = dateTimeService;
		}

		public async Task<Unit> Handle(SeedCommand request, CancellationToken cancellationToken)
		{
			var thread = await _dbContext.Threads.FirstOrDefaultAsync(e => e.Title == "New Thread", cancellationToken);
			if (thread != null)
			{
				return Unit.Value;
			}

			var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Username == "Admin",
				cancellationToken: cancellationToken) ?? new UserBuilder()
				.WithPassword(BCrypt.Net.BCrypt.EnhancedHashPassword("admin"))
				.WithUsername("Admin")
				.WithEmailAddress("Email@Address.com")
				.WithJoinDateUtc(_dateTimeService.UtcNow)
				.Build();

			var subForum = await _dbContext.SubForums.FirstOrDefaultAsync(e => e.Title == "Main", cancellationToken) ??
			               new SubForumBuilder()
				               .WithName("Main")
				               .Build();

			var generalTopic =
				await _dbContext.Topics.FirstOrDefaultAsync(e => e.Title == "General", cancellationToken) ??
				new TopicBuilder()
					.WithName("General")
					.UnderSubForum(subForum)
					.Build();

			var postBuilder = new PostBuilder()
				.WithContent("Hello World")
				.CreatedDateUtc(_dateTimeService.UtcNow)
				.CreatedByUser(user);

			thread = new ThreadBuilder()
				.WithTitle("New Thread")
				.InTopic(generalTopic)
				.WithPost(postBuilder)
				.CreatedBy(user)
				.CreatedDate(_dateTimeService.UtcNow)
				.Build();

			_dbContext.Threads.Add(thread);
			await _dbContext.SaveChangesAsync(cancellationToken);
			return Unit.Value;
		}
	}
}
