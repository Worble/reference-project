using System.Threading.Tasks;
using Forum.Application.Forum.Commands.Seed;
using Forum.Presentation.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Presentation.Controllers
{
	public class SeedController : ForumControllerBase
	{
		private readonly IMediator _mediator;

		public SeedController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> Seed()
		{
			await _mediator.Send(new SeedCommand());
			return Ok();
		}
	}
}
