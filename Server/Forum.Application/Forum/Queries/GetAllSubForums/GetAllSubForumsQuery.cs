using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Forum.Application.Abstractions.DbContexts;
using Forum.Application.Abstractions.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Application.Forum.Queries.GetAllSubForums
{
	public class GetAllSubForumsQuery : ICacheableQuery, IRequest<IEnumerable<GetAllSubForumsViewModel>>
	{}

	public class
		GetAllSubForumsQueryHandler : IRequestHandler<GetAllSubForumsQuery, IEnumerable<GetAllSubForumsViewModel>>
	{
		private readonly IForumDbContext _dbContext;

		public GetAllSubForumsQueryHandler(IForumDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<GetAllSubForumsViewModel>> Handle(GetAllSubForumsQuery request,
			CancellationToken cancellationToken)
		{
			return await _dbContext.SubForums
				.Include(subForum => subForum.Topics.Where(topic => topic.Parent == null))
				.Select(subForum => new GetAllSubForumsViewModel
				{
					Id = subForum.Id,
					Title = subForum.Title,
					Topics = subForum.Topics.Select(child =>
						new GetAllSubForumsTopicViewModel {Id = child.Id, Title = child.Title})
				})
				.ToListAsync(cancellationToken: cancellationToken);
		}
	}
}
