using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Presentation
{
	[ApiController]
	[Route("[controller]")]
	public class SeedController : ControllerBase
	{
		private readonly IMediator _mediator;

		public SeedController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public IActionResult Seed()
		{
			_mediator.Send(new SeedCommand());
			return Ok();
		}
	}
}
