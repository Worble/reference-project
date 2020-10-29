using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Forum.Application.Abstractions.DbContexts;
using Forum.Application.Abstractions.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Application.Forum.Queries.GetFrontPage
{
	public class GetFrontPageQuery : ICacheableQuery, IRequest<GetFrontPageViewModel>
	{}

	public class GetFrontPageQueryHandler : IRequestHandler<GetFrontPageQuery, GetFrontPageViewModel>
	{
		private readonly IForumDbContext _dbContext;

		public GetFrontPageQueryHandler(IForumDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<GetFrontPageViewModel> Handle(GetFrontPageQuery request, CancellationToken cancellationToken)
		{
			var topics = await _dbContext.Topics
				.Include(topic => topic.Children)
				.Where(topic => topic.Parent == null)
				.Select(topic => new GetFrontPageTopicViewModel
				{
					Title = topic.Name,
					Children = topic.Children.Select(child => new GetFrontPageTopicViewModel {Title = child.Name})
				})
				.ToListAsync(cancellationToken: cancellationToken);
			return new GetFrontPageViewModel {Topics = topics};
		}
	}
}
