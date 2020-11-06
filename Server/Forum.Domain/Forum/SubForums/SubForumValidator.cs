using FluentValidation;

namespace Forum.Domain.Forum.SubForums
{
	public class SubForumValidator : AbstractValidator<SubForum>
	{
		public SubForumValidator()
		{
			RuleFor(e => e.Title).NotEmpty();
		}
	}
}
