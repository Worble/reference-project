using System.Threading.Tasks;
using Forum.Application.Forum.Commands.CreatePost;
using Forum.Presentation.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Presentation.Controllers
{
	public class PostsController : ForumControllerBase
	{
		private readonly IMediator _mediator;

		public PostsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[Authorize, HttpPost("create")]
		public async Task<ActionResult> CreatePost(CreatePostCommand command)
		{
			await _mediator.Send(command);
			return Ok();
		}
	}
}
