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
	{}

	public class GetEmployeeListQueryHandler : IRequestHandler<GetAllUsersQuery, List<GetAllUsersViewModel>>
	{
		private readonly IForumDbContext _dbContext;

		public GetEmployeeListQueryHandler(IForumDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<GetAllUsersViewModel>> Handle(GetAllUsersQuery request,
			CancellationToken cancellationToken) =>
			await _dbContext.Users
				.Select(e => new GetAllUsersViewModel {Username = e.Username, Id = e.Id})
				.ToListAsync(cancellationToken);
	}
}
