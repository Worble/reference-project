using FluentValidation;

namespace Forum.Domain.Forum.Posts
{
	public class PostValidator : AbstractValidator<Post>
	{
		public PostValidator()
		{
			RuleFor(e => e.Content).NotEmpty();
			RuleFor(e => e.CreatedBy).NotEmpty();
			RuleFor(e => e.Thread).NotEmpty();
			RuleFor(e => e.CreatedDateUtc).NotEmpty();
		}
	}
}
