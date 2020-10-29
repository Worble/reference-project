using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Forum.Application.Abstractions.DbContexts;
using Forum.Application.Abstractions.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Application.Forum.Queries.GetTopic
{
	public class GetTopicQuery : ICacheableQuery, IRequest<GetTopicViewModel>
	{
		public string TopicName { get; set; }
	}

	public class GetFrontPageQueryHandler : IRequestHandler<GetTopicQuery, GetTopicViewModel>
	{
		private readonly IForumDbContext _dbContext;

		public GetFrontPageQueryHandler(IForumDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public Task<GetTopicViewModel> Handle(GetTopicQuery request, CancellationToken cancellationToken)
		{
			return _dbContext.Topics
				.Where(topic => EF.Functions.ILike(topic.Name, request.TopicName))
				.Include(topic => topic.Children)
				.Include(topic => topic.Threads)
				.ThenInclude(thread => thread.CreatedBy)
				.Select(topic => new GetTopicViewModel
				{
					Name = topic.Name,
					Threads = topic.Threads.Select(thread => new GetTopicThreadViewModel
					{
						Id = thread.Id,
						Title = thread.Title,
						DateCreated = thread.CreatedDate,
						CreatedBy = new GetTopicUserViewModel
						{
							Id = thread.CreatedBy!.Id, Username = thread.CreatedBy!.Username
						}
					})
				})
				.FirstOrDefaultAsync(cancellationToken);
		}
	}
}
