using System.Threading.Tasks;
using Forum.Application.Forum.Queries.GetTopic;
using Forum.Presentation.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Presentation.Controllers
{
	public class TopicsController : ForumControllerBase
	{
		private readonly IMediator _mediator;

		public TopicsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("{topicId}")]
		public async Task<ActionResult<GetTopicViewModel>> GetTopic(int topicId)
		{
			return await _mediator.Send(new GetTopicQuery {Id = topicId});
		}
	}
}
