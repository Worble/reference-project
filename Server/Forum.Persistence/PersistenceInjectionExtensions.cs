using System;
using System.Linq;
using Audit.Core;
using FluentValidation.Internal;
using Forum.Application.Abstractions.DbContexts;
using Forum.Application.Common.Audit;
using Forum.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.Persistence
{
	public static class PersistenceInjectionExtensions
	{
		public static IServiceCollection AddPersistenceLayer(this IServiceCollection services,
			string databaseConnection)
		{
			services
				.AddDbContext<ForumDbContext>(options => options.UseNpgsql(databaseConnection))
				.AddScoped<IForumDbContext>(provider => provider.GetService<ForumDbContext>()!);

			Configuration.Setup()
				.UseEntityFramework(x => x
					.AuditTypeMapper(t => typeof(AuditLog))
					.AuditEntityAction<AuditLog>((ev, entry, entity) =>
					{
						var (_, primaryKey) = entry.PrimaryKey.FirstOrDefault();
						entity.AuditData = entry.ToJson();
						entity.EntityType = entry.EntityType.Name;
						entity.EntityPrimaryKey = primaryKey != null
							? primaryKey.ToString()
							: string.Empty;
						entity.AuditDate = DateTime.Now;
					})
					.IgnoreMatchedProperties(true));

			Audit.EntityFramework.Configuration.Setup()
				.ForContext<ForumDbContext>(_ => _
					.AuditEventType("EF:{context}"))
				.UseOptOut();

			return services;
		}
	}

	public static class Extensions
	{
		public static bool IsDefault<T>(this T value) where T : struct
		{
			return value.Equals(default(T));
		}
	}
}
