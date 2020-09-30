using Forum.Application.Abstractions.Dates;
using Forum.Application.Abstractions.Identity;
using Forum.Application.Abstractions.Serialization;
using Forum.Infrastructure.Dates;
using Forum.Infrastructure.Identity;
using Forum.Infrastructure.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.Infrastructure
{
	public static class InfrastructureInjectionExtensions
	{
		public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services) =>
			services.AddTransient<IDateTimeService, DateTimeService>()
				.AddTransient<ICurrentUserService, CurrentUserService>()
				.AddTransient<IJsonSerializer, SystemJsonSerializer>()
				.AddTransient<IByteSerializer, ByteSerializer>();
	}
}
