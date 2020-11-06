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
		public int Id { get; set; }
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
				.Where(topic => topic.Id == request.Id)
				.Include(topic => topic.Children)
				.Include(topic => topic.Threads)
				.ThenInclude(thread => thread.CreatedBy)
				.Include(topic => topic.Parent)
				.Select(topic => new GetTopicViewModel
				{
					Id = topic.Id,
					Title = topic.Title,
					Parent = topic.Parent != null
						? new GetTopicViewModel {Id = topic.Parent.Id, Title = topic.Parent.Title,}
						: null,
					Children =
						topic.Children.Select(childTopic =>
							new GetTopicViewModel {Id = childTopic.Id, Title = childTopic.Title}),
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
