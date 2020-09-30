using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Forum.Application.Abstractions.Dates;
using Forum.Application.Abstractions.DbContexts;
using Forum.Application.Abstractions.Mapping;
using Forum.Application.Common.Topics;
using Forum.Application.Common.Users;
using Forum.Domain.Forum.Topics;
using Forum.Domain.Forum.Users;
using MediatR;

namespace Forum.Presentation
{
	public class SeedCommand : IRequest
	{}

	public class SeedCommandHandler : IRequestHandler<SeedCommand>
	{
		private readonly IDateTimeService _dateTimeService;
		private readonly IForumDbContext _dbContext;
		private readonly IEnumerable<IDomainEntityToDbEntityMapper> _mappers;

		public SeedCommandHandler(IForumDbContext dbContext,
			IDateTimeService dateTimeService,
			IEnumerable<IDomainEntityToDbEntityMapper> mappers)
		{
			_dbContext = dbContext;
			_dateTimeService = dateTimeService;
			_mappers = mappers;
		}

		public async Task<Unit> Handle(SeedCommand request, CancellationToken cancellationToken)
		{
			// ADD ADMIN USER
			var adminDomain = await new UserBuilder()
				.WithUsername("admin")
				.WithPassword(BCrypt.Net.BCrypt.HashPassword("password"))
				.WithEmailAddress("admin@email.com")
				.WithJoinDateUtc(_dateTimeService.UtcNow)
				.Build();

			var adminEntity = _mappers
				.OfType<IDomainEntityToDbEntityMapper<User, UserEntity>>()
				.First()
				.MapToDbEntity(adminDomain);

			_dbContext.Users.Add(adminEntity);

			// ADD TOPICS
			var generalTopicDomain = await new TopicBuilder()
				.WithName("General")
				.Build();
			var gamesTopicDomain = await new TopicBuilder()
				.WithName("Games")
				.Build();
			var fpsTopicDomain = await new TopicBuilder()
				.WithName("FPS")
				.WithParent(gamesTopicDomain)
				.Build();
			var rpgTopicDomain = await new TopicBuilder()
				.WithName("RPG")
				.WithParent(gamesTopicDomain)
				.Build();

			var topicMapper = _mappers
				.OfType<IDomainEntityToDbEntityMapper<Topic, TopicEntity>>()
				.First();

			var topicsEntities =
				new[] {generalTopicDomain, gamesTopicDomain, fpsTopicDomain, rpgTopicDomain}
					.Select(e => topicMapper.MapToDbEntity(e));

			_dbContext.Topics.AddRange(topicsEntities);

			// ADD THREADS

			await _dbContext.SaveChangesAsync(cancellationToken);

			return new Unit();
		}
	}
}
