using System.Threading.Tasks;
using Forum.Application.Abstractions.DbContexts;
using Forum.Application.Abstractions.Identity;
using Forum.Domain.Forum.Users;

namespace Forum.Infrastructure.Identity
{
	public class CurrentUserService : ICurrentUserService
	{
		private readonly IForumDbContext _dbContext;
		private User? currentUser;

		public CurrentUserService(IForumDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task SetCurrentUser(int userId)
		{
			currentUser = await _dbContext.Users.FindAsync(userId);
		}

		public bool TryGetCurrentUser(out User? user)
		{
			user = currentUser;
			return user != null;
		}
	}
}
