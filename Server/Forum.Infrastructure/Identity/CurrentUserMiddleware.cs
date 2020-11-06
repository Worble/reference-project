using System.Security.Claims;
using System.Threading.Tasks;
using Forum.Application.Abstractions.Identity;
using Microsoft.AspNetCore.Http;

namespace Forum.Infrastructure.Identity
{
	public class CurrentUserMiddleware
	{
		private readonly RequestDelegate _next;

		public CurrentUserMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context, ICurrentUserService currentUserService)
		{
			var userIdClaim = context.User.FindFirst(e => e.Type == ClaimTypes.NameIdentifier);
			if (userIdClaim == null)
			{
				await _next(context);
				return;
			}

			await currentUserService.SetCurrentUser(int.Parse(userIdClaim.Value));
			await _next(context);
		}
	}
}
