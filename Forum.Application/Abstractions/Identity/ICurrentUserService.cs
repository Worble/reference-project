using Forum.Domain.Forum.Users;

namespace Forum.Application.Abstractions.Identity
{
	public interface ICurrentUserService
	{
		bool TryGetCurrentUser(out User user);
	}
}
