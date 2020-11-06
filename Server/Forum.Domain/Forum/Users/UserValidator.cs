using FluentValidation;

namespace Forum.Domain.Forum.Users
{
	public class UserValidator : AbstractValidator<User>
	{
		public UserValidator()
		{
			RuleFor(e => e.Username).NotEmpty();
			RuleFor(e => e.EmailAddress).NotEmpty().EmailAddress();
			RuleFor(e => e.Password).NotEmpty();
			RuleFor(e => e.JoinDateUtc).NotEmpty();
		}
	}
}
