using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Presentation.Controllers
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
		public async Task<IActionResult> Seed()
		{
			await _mediator.Send(new SeedCommand());
			return Ok();
		}
	}
}
