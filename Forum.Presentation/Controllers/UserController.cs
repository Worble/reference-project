using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.Application.Forum.Queries.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Presentation.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
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
