using System;
using System.Threading.Tasks;
using Forum.Domain.Abstractions.Models;

namespace Forum.Domain.Forum.Users
{
	public class UserBuilder : IBuilder<User>
	{
		private readonly User _user = new User();

		public async Task<User> Build()
		{
			_user.Validate();
			_user.AddDomainEvent(new UserRegisteredEvent(_user));
			await _user
				.DispatchDomainEventsAsync()
				.ConfigureAwait(false);
			return _user;
		}

		public UserBuilder WithEmailAddress(string emailAddress)
		{
			_user.EmailAddress = emailAddress ?? throw new ArgumentNullException(nameof(emailAddress));
			return this;
		}

		public UserBuilder WithJoinDateUtc(DateTime joinDate)
		{
			_user.JoinDateUtc = joinDate;
			return this;
		}

		public UserBuilder WithPassword(string password)
		{
			_user.Password = password ?? throw new ArgumentNullException(nameof(password));
			return this;
		}

		public UserBuilder WithUsername(string username)
		{
			_user.Username = username ?? throw new ArgumentNullException(nameof(username));
			return this;
		}
	}
}
