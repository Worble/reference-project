using System.Threading;
using System.Threading.Tasks;
using Forum.Application.Abstractions.Dates;
using Forum.Application.Abstractions.DbContexts;
using Forum.Application.Abstractions.Identity;
using Forum.Domain.Forum.Posts;
using MediatR;

namespace Forum.Application.Forum.Commands.CreatePost
{
	public class CreatePostCommand : IRequest
	{
		public string Content { get; set; }
		public int ThreadId { get; set; }
	}

	public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand>
	{
		private readonly IForumDbContext _context;
		private readonly ICurrentUserService _currentUserService;
		private readonly IDateTimeService _dateTimeService;

		public CreatePostCommandHandler(IDateTimeService dateTimeService, ICurrentUserService currentUserService,
			IForumDbContext context)
		{
			_dateTimeService = dateTimeService;
			_currentUserService = currentUserService;
			_context = context;
		}

		public async Task<Unit> Handle(CreatePostCommand request, CancellationToken cancellationToken)
		{
			if (!_currentUserService.TryGetCurrentUser(out var user))
			{
				throw new CreatePostCommandException("Could not get current user");
			}

			var post = new PostBuilder()
				.InThread(request.ThreadId)
				.WithContent(request.Content)
				.CreatedDateUtc(_dateTimeService.UtcNow)
				.CreatedByUser(user!.Id)
				.Build();

			_context.Posts.Add(post);
			await _context.SaveChangesAsync(cancellationToken);
			return Unit.Value;
		}
	}
}
