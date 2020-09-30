using System;
using System.Linq;
using Forum.Application;
using Forum.Domain;
using Forum.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Forum.Presentation
{
	public class Startup
	{
		private const string DatabaseConnectionName = "Forum";

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			serviceProvider.WireUpDomainEventHandlers();

			app.UseSerilogRequestLogging();

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) =>
			services
				.AddDomainLayer()
				.AddApplicationLayer(
					Configuration.GetSection("Application.Logging"),
					Configuration.GetSection("Application.Caching"),
					Configuration.GetSection("Application.Performance"),
					Configuration.GetSection("Application.Validation"))
				.AddInfrastructureLayer()
				.AddPersistenceLayer(Configuration.GetConnectionString(DatabaseConnectionName))
				.AddDistributedMemoryCache()
				.AddMediatR(new[] {typeof(DomainLayer), typeof(ApplicationLayer)}.Select(t => t.Assembly).ToArray())
				.AddSwaggerGen(c =>
				{
					c.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"});
				})
				.AddControllers();
	}
}
