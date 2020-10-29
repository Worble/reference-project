using FluentValidation;

namespace Forum.Application.Forum.Queries.Login
{
	public class LoginQueryValidator : AbstractValidator<LoginQuery>
	{
		public LoginQueryValidator()
		{
			RuleFor(e => e.EmailAddressOrUsername).NotEmpty();
			RuleFor(e => e.Password).NotEmpty();
		}
	}
}
