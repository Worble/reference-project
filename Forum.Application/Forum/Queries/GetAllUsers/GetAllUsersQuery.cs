using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Forum.Application.Abstractions.DbContexts;
using Forum.Application.Abstractions.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Application.Forum.Queries.GetAllUsers
{
	public class GetAllUsersQuery : ICacheableQuery, IRequest<List<GetAllUsersViewModel>>
	{
		public bool BypassCache { get; }
		public string CacheKey { get; } = string.Empty;
		public bool RefreshCachedEntry { get; }
		public bool ReplaceCachedEntry { get; }
	}

	public class GetEmployeeListQueryHandler : IRequestHandler<GetAllUsersQuery, List<GetAllUsersViewModel>>
	{
		private readonly IForumDbContext _forumDbContext;

		public GetEmployeeListQueryHandler(IForumDbContext forumDbContext)
		{
			_forumDbContext = forumDbContext;
		}

		public async Task<List<GetAllUsersViewModel>> Handle(GetAllUsersQuery request,
			CancellationToken cancellationToken) =>
			await _forumDbContext.Users
				.Select(e => new GetAllUsersViewModel {Username = e.Username})
				.ToListAsync(cancellationToken);
	}
}
