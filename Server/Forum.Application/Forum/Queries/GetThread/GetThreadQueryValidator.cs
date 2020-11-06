using FluentValidation;

namespace Forum.Application.Forum.Queries.GetThread
{
	public class GetThreadQueryValidator : AbstractValidator<GetThreadQuery>
	{
		public GetThreadQueryValidator()
		{
			RuleFor(e => e.ThreadId).NotEmpty();
		}
	}
}
