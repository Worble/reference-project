using System.Threading.Tasks;
using Forum.Application.Forum.Queries.GetThread;
using Forum.Presentation.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Presentation.Controllers
{
	public class ThreadsController : ForumControllerBase
	{
		private readonly IMediator _mediator;

		public ThreadsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("{threadId}")]
		public async Task<ActionResult<GetThreadViewModel>> GetThread(int threadId)
		{
			return await _mediator.Send(new GetThreadQuery {ThreadId = threadId});
		}
	}
}
