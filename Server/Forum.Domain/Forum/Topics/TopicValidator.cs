using FluentValidation;

namespace Forum.Domain.Forum.Topics
{
	public class TopicValidator : AbstractValidator<Topic>
	{
		public TopicValidator()
		{
			RuleFor(e => e.Title).NotEmpty();
			RuleFor(e => e.SubForumId).NotEmpty().When(e => e.SubForum == null);
			RuleFor(e => e.SubForum).NotEmpty().When(e => e.SubForumId == default);
		}
	}
}
