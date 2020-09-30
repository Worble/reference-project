using FluentValidation;

namespace Forum.Domain.Forum.Threads
{
	public class ThreadValidator : AbstractValidator<Thread>
	{
		public ThreadValidator()
		{
			RuleFor(e => e.Title).NotEmpty();
			RuleFor(e => e.Topic).NotEmpty();
			RuleFor(e => e.Posts).NotEmpty();
		}
	}
}
