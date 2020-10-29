using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Forum.Application.Abstractions.DbContexts;
using Forum.Application.Abstractions.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Application.Forum.Queries.GetThread
{
	public class GetThreadQuery : ICacheableQuery, IRequest<GetThreadViewModel>
	{
		public int ThreadId { get; set; }
	}

	public class GetThreadQueryHandler : IRequestHandler<GetThreadQuery, GetThreadViewModel>
	{
		private readonly IForumDbContext _dbContext;

		public GetThreadQueryHandler(IForumDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public Task<GetThreadViewModel> Handle(GetThreadQuery request, CancellationToken cancellationToken)
		{
			return _dbContext.Threads
				.Where(thread => thread.Id == request.ThreadId)
				.Include(thread => thread.CreatedBy)
				.Include(thread => thread.Posts)
				.ThenInclude(post => post.CreatedBy)
				.Select(thread => new GetThreadViewModel
				{
					Id = thread.Id,
					Title = thread.Title,
					CreatedDate = thread.CreatedDate,
					CreatedBy =
						new GetThreadUserViewModel {Id = thread.CreatedBy!.Id, Username = thread.CreatedBy!.Username},
					Posts = thread.Posts.Select(e => new GetThreadPostViewModel
					{
						Id = e.Id,
						Content = e.Content,
						CreatedDate = e.CreatedDateUtc,
						CreatedBy = new GetThreadUserViewModel
						{
							Id = e.CreatedBy!.Id, Username = e.CreatedBy!.Username
						}
					})
				})
				.FirstOrDefaultAsync(cancellationToken);
		}
	}
}
