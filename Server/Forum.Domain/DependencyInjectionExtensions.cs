using System;
using Forum.Domain.Abstractions.Events;
using Forum.Domain.Events;
using Forum.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.Domain
{
	public static class DependencyInjectionExtensions
	{
		public static IServiceCollection AddDomainLayer(this IServiceCollection services)
		{
			services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
			return services;
		}

		public static IServiceProvider WireUpDomainEventHandlers(this IServiceProvider serviceProvider)
		{
			DomainEntity.WireUpDispatcher(serviceProvider.GetRequiredService<IDomainEventDispatcher>());
			return serviceProvider;
		}
	}
}
