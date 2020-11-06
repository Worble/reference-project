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
			RuleFor(e => e.CreatedById).NotEmpty().When(e => e.CreatedBy == null);
			RuleFor(e => e.CreatedBy).NotEmpty().When(e => e.CreatedById == default);
			RuleFor(e => e.CreatedDate).NotEmpty();
		}
	}
}
