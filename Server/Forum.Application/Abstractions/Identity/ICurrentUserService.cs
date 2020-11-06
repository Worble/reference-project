using System.Threading.Tasks;
using Forum.Domain.Forum.Users;

namespace Forum.Application.Abstractions.Identity
{
	public interface ICurrentUserService
	{
		Task SetCurrentUser(int userId);
		bool TryGetCurrentUser(out User? user);
	}
}
