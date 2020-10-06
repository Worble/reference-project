using System;
using System.Threading;
using System.Threading.Tasks;
using Forum.Application.Abstractions.Dates;
using Forum.Application.Abstractions.DbContexts;
using Forum.Application.Abstractions.Mapping;
using Forum.Domain.Forum.Posts;
using Forum.Domain.Forum.Topics;
using Forum.Domain.Forum.Users;
using MediatR;
using Thread = Forum.Domain.Forum.Threads.Thread;

namespace Forum.Presentation
{
	public class SeedCommand : IRequest
	{
	}

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
			throw new NotImplementedException();
		}
	}
}
