using Forum.Application.Behaviours;
using Forum.Application.Models.RequestCacheConfigurations;
using Forum.Application.Models.RequestLoggingConfigurations;
using Forum.Application.Models.RequestPerformanceConfigurations;
using Forum.Application.Models.RequestValidationConfigurations;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.Application
{
	public static class DependencyInjectionExtensions
	{
		public static IServiceCollection AddApplicationLayer(this IServiceCollection services,
			IConfigurationSection requestLoggerConfigSection, IConfigurationSection requestCachingConfigSection,
			IConfigurationSection requestPerformanceConfigSection,
			IConfigurationSection requestValidationConfigSection)
		{
			return services.Configure<RequestLoggingConfiguration>(requestLoggerConfigSection)
				.Configure<RequestCacheConfiguration>(requestCachingConfigSection)
				.Configure<RequestPerformanceConfiguration>(requestPerformanceConfigSection)
				.Configure<RequestValidationConfiguration>(requestValidationConfigSection)
				.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLogger<,>))
				.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestCachingBehavior<,>))
				.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>))
				.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
		}
	}
}
