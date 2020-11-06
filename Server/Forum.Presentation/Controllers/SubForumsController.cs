using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.Application.Forum.Queries.GetAllSubForums;
using Forum.Presentation.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Presentation.Controllers
{
	public class SubForumsController : ForumControllerBase
	{
		private readonly IMediator _mediator;

		public SubForumsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public Task<IEnumerable<GetAllSubForumsViewModel>> Get()
		{
			return _mediator.Send(new GetAllSubForumsQuery());
		}
	}
}
