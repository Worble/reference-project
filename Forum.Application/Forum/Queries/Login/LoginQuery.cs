using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Forum.Application.Abstractions.DbContexts;
using Forum.Common.JsonConverters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Forum.Application.Forum.Queries.Login
{
	public class LoginQuery : IRequest<LoginUserModel>
	{
		public string EmailAddressOrUsername { get; set; } = string.Empty;

		[JsonConverter(typeof(SensitiveInformationConverter))]
		public string Password { get; set; } = string.Empty;
	}

	public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginUserModel>
	{
		private readonly IForumDbContext _dbContext;
		private readonly ILogger<LoginQueryHandler> _logger;

		public LoginQueryHandler(IForumDbContext dbContext, ILogger<LoginQueryHandler> logger)
		{
			_dbContext = dbContext;
			_logger = logger;
		}

		public async Task<LoginUserModel> Handle(LoginQuery request, CancellationToken cancellationToken)
		{
			var user = await _dbContext.Users.FirstOrDefaultAsync(
				e => EF.Functions.ILike(e.Username, $"{request.EmailAddressOrUsername}")
				     || EF.Functions.ILike(e.EmailAddress, $"{request.EmailAddressOrUsername}"),
				cancellationToken);

			if (user == null || !BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.Password))
			{
				throw new LoginFailedException();
			}

			return new LoginUserModel {Id = user.Id, Username = user.Username, EmailAddress = user.EmailAddress};
		}
	}
}
