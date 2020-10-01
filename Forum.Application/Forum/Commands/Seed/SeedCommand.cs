using System.Threading;
using System.Threading.Tasks;
using Forum.Application.Abstractions.Dates;
using Forum.Application.Abstractions.DbContexts;
using Forum.Application.Abstractions.Mapping;
using Forum.Application.Common.Posts;
using Forum.Application.Common.Threads;
using Forum.Application.Common.Topics;
using Forum.Application.Common.Users;
using Forum.Domain.Forum.Posts;
using Forum.Domain.Forum.Threads;
using Forum.Domain.Forum.Topics;
using Forum.Domain.Forum.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Thread = Forum.Domain.Forum.Threads.Thread;

namespace Forum.Presentation
{
	public class SeedCommand : IRequest
	{}

	public class SeedCommandHandler : IRequestHandler<SeedCommand>
	{
		private readonly IDateTimeService _dateTimeService;
		private readonly IForumDbContext _dbContext;
		private readonly IDomainEntityToDbEntityMapper<Post, PostEntity> _postMapper;
		private readonly IDomainEntityToDbEntityMapper<Thread, ThreadEntity> _threadMapper;
		private readonly IDomainEntityToDbEntityMapper<Topic, TopicEntity> _topicMapper;
		private readonly IDomainEntityToDbEntityMapper<User, UserEntity> _userMapper;

		public SeedCommandHandler(IForumDbContext dbContext,
			IDateTimeService dateTimeService,
			IDomainEntityToDbEntityMapper<User, UserEntity> userMapper,
			IDomainEntityToDbEntityMapper<Topic, TopicEntity> topicMapper,
			IDomainEntityToDbEntityMapper<Thread, ThreadEntity> threadMapper,
			IDomainEntityToDbEntityMapper<Post, PostEntity> postMapper)
		{
			_dbContext = dbContext;
			_dateTimeService = dateTimeService;
			_userMapper = userMapper;
			_topicMapper = topicMapper;
			_threadMapper = threadMapper;
			_postMapper = postMapper;
		}

		public async Task<Unit> Handle(SeedCommand request, CancellationToken cancellationToken)
		{
			// ADD ADMIN USER
			var adminEntity = await _dbContext
				.Users
				.FirstOrDefaultAsync(e => e.Username == "admin", cancellationToken);
			if (adminEntity == null)
			{
				var adminDomain = await new UserBuilder()
					.WithUsername("admin")
					.WithPassword(BCrypt.Net.BCrypt.HashPassword("password"))
					.WithEmailAddress("admin@email.com")
					.WithJoinDateUtc(_dateTimeService.UtcNow)
					.Build();

				adminEntity = _userMapper
					.MapToDbEntity(adminDomain);

				adminEntity = _dbContext.Users.Add(adminEntity).Entity;
				await _dbContext.SaveChangesAsync(cancellationToken);
			}

			// ADD TOPICS
			if (!await _dbContext.Topics.AnyAsync(cancellationToken))
			{
				var mainTopic = await new TopicBuilder()
					.WithName("Main")
					.Build();
				var generalTopicDomain = await new TopicBuilder()
					.WithName("General")
					.WithParent(mainTopic)
					.Build();
				var gamesTopicDomain = await new TopicBuilder()
					.WithName("Games")
					.WithParent(mainTopic)
					.Build();
				var fpsTopicDomain = await new TopicBuilder()
					.WithName("FPS")
					.WithParent(gamesTopicDomain)
					.Build();
				var rpgTopicDomain = await new TopicBuilder()
					.WithName("RPG")
					.WithParent(gamesTopicDomain)
					.Build();

				var mainTopicEntity = _topicMapper.MapToDbEntity(mainTopic);
				var generalTopicEntity = _topicMapper.MapToDbEntity(generalTopicDomain);
				var gamesTopicEntity = _topicMapper.MapToDbEntity(gamesTopicDomain);
				var fpsTopicEntity = _topicMapper.MapToDbEntity(fpsTopicDomain);
				var rpgTopicEntity = _topicMapper.MapToDbEntity(rpgTopicDomain);

				_dbContext.Topics.AddRange(new[]
				{
					mainTopicEntity, generalTopicEntity, gamesTopicEntity, fpsTopicEntity, rpgTopicEntity
				});
				await _dbContext.SaveChangesAsync(cancellationToken);
			}

			var topicEntity = await _dbContext.Topics.FirstOrDefaultAsync(e => e.Name == "General", cancellationToken);

			// ADD THREADS
			var threadDomain = await new ThreadBuilder()
				.InTopic(_topicMapper.MapToDomainEntity(topicEntity))
				.WithTitle("Hello World")
				.WithPost(new PostBuilder()
					.WithContent("Hello!")
					.CreatedByUser(_userMapper.MapToDomainEntity(adminEntity))
					.CreatedDateUtc(_dateTimeService.UtcNow))
				.Build();

			var threadEntity = _threadMapper
				.MapToDbEntity(threadDomain);

			_dbContext.Threads.Add(threadEntity);

			// SAVE
			await _dbContext.SaveChangesAsync(cancellationToken);

			return new Unit();
		}
	}
}
