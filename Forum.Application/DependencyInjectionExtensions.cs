using Forum.Application.Abstractions.Mapping;
using Forum.Application.Behaviours;
using Forum.Application.Common.Posts;
using Forum.Application.Common.Threads;
using Forum.Application.Common.Topics;
using Forum.Application.Common.Users;
using Forum.Application.Models.RequestCacheConfigurations;
using Forum.Application.Models.RequestLoggingConfigurations;
using Forum.Application.Models.RequestPerformanceConfigurations;
using Forum.Application.Models.RequestValidationConfigurations;
using Forum.Domain.Forum.Posts;
using Forum.Domain.Forum.Threads;
using Forum.Domain.Forum.Topics;
using Forum.Domain.Forum.Users;
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
			IConfigurationSection requestValidationConfigSection) =>
			services.AddSingleton<IDomainEntityToDbEntityMapper<User, UserEntity>, UserDomainToDbEntityMapper>()
				.AddSingleton<IDomainEntityToDbEntityMapper<Topic, TopicEntity>, TopicDomainToDbEntityMapper>()
				.AddSingleton<IDomainEntityToDbEntityMapper<Thread, ThreadEntity>, ThreadDomainToDbEntityMapper>()
				.AddSingleton<IDomainEntityToDbEntityMapper<Post, PostEntity>, PostDomainToDbEntityMapper>()
				.AddSingleton<IDomainEntityToDbEntityMapper<Topic, TopicEntity>, TopicDomainToDbEntityMapper>()
				.Configure<RequestLoggingConfiguration>(requestLoggerConfigSection)
				.Configure<RequestCacheConfiguration>(requestCachingConfigSection)
				.Configure<RequestPerformanceConfiguration>(requestPerformanceConfigSection)
				.Configure<RequestValidationConfiguration>(requestValidationConfigSection)
				.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLogger<,>))
				.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestCachingBehavior<,>))
				.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>))
				.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
	}
}
