using Forum.Application.Abstractions.DbContexts;
using Forum.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.Infrastructure
{
	public static class PersistenceInjectionExtensions
	{
		public static IServiceCollection AddPersistenceLayer(this IServiceCollection services,
			string databaseConnection)
		{
			return services
				.AddDbContext<ForumDbContext>(options => options.UseSqlite(databaseConnection))
				.AddScoped<IForumDbContext>(provider => provider.GetService<ForumDbContext>()!);
		}
	}
}
