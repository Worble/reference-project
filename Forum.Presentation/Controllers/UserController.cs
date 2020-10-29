using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.Application.Forum.Queries.GetAllUsers;
using Forum.Presentation.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Presentation.Controllers
{
	public class UserController : ForumControllerBase
	{
		private readonly IMediator _mediator;

		public UserController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IEnumerable<GetAllUsersViewModel>> Get()
		{
			return await _mediator.Send(new GetAllUsersQuery());
		}
	}
}
