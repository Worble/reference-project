using FluentValidation;

namespace Forum.Application.Forum.Queries.GetTopic
{
	public class GetTopicQueryValidator : AbstractValidator<GetTopicQuery>
	{
		public GetTopicQueryValidator()
		{
			RuleFor(e => e.TopicName).NotEmpty();
		}
	}
}
