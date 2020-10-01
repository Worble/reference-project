﻿using Forum.Application.Abstractions.Identity;
using Forum.Domain.Forum.Users;

namespace Forum.Infrastructure.Identity
{
	public class CurrentUserService : ICurrentUserService
	{
		public bool TryGetCurrentUser(out User? user)
		{
			user = null;
			return false;
		}
	}
}
