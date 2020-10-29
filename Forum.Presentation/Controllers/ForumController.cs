using System.Threading.Tasks;
using Forum.Application.Forum.Queries.GetFrontPage;
using Forum.Application.Forum.Queries.GetThread;
using Forum.Application.Forum.Queries.GetTopic;
using Forum.Presentation.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Presentation.Controllers
{
	public class ForumController : ForumControllerBase
	{
		private readonly IMediator _mediator;

		public ForumController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public Task<GetFrontPageViewModel> Get()
		{
			return _mediator.Send(new GetFrontPageQuery());
		}

		[HttpGet("{topic}")]
		public async Task<ActionResult<GetTopicViewModel>> GetTopic(string topic)
		{
			if (string.IsNullOrWhiteSpace(topic))
			{
				return BadRequest();
			}

			return await _mediator.Send(new GetTopicQuery {TopicName = topic});
		}

		[HttpGet("{topic}/{threadId}")]
		public async Task<ActionResult<GetThreadViewModel>> GetTopic(string topic, int threadId)
		{
			if (string.IsNullOrWhiteSpace(topic) || threadId == default)
			{
				return BadRequest();
			}

			return await _mediator.Send(new GetThreadQuery {ThreadId = threadId});
		}
	}
}
