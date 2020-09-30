using FluentValidation;

namespace Forum.Domain.Forum.Topics
{
	public class TopicValidator : AbstractValidator<Topic>
	{
		public TopicValidator()
		{
			RuleFor(e => e.Name).NotEmpty();
		}
	}
}
