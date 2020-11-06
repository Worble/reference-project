using FluentValidation;

namespace Forum.Domain.Forum.Posts
{
	public class PostValidator : AbstractValidator<Post>
	{
		public PostValidator()
		{
			RuleFor(e => e.Content).NotEmpty();
			RuleFor(e => e.CreatedById).NotEmpty().When(e => e.CreatedBy == null);
			RuleFor(e => e.CreatedBy).NotEmpty().When(e => e.CreatedById == default);
			RuleFor(e => e.ThreadId).NotEmpty().When(e => e.Thread == null);
			RuleFor(e => e.Thread).NotEmpty().When(e => e.ThreadId == default);
			RuleFor(e => e.CreatedDateUtc).NotEmpty();
		}
	}
}
