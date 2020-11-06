using FluentValidation;

namespace Forum.Application.Forum.Commands.CreatePost
{
	public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
	{
		public CreatePostCommandValidator()
		{
			RuleFor(e => e.Content).NotEmpty();
			RuleFor(e => e.ThreadId).NotEmpty();
		}
	}
}
