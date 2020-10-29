using System;
using System.Linq;
using CorrelationId;
using CorrelationId.DependencyInjection;
using Forum.Application;
using Forum.Domain;
using Forum.Infrastructure;
using Forum.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
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

		private IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(b =>
			{
				b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
			});

			app.UseHttpsRedirection();

			app.UseCorrelationId();

			serviceProvider.WireUpDomainEventHandlers();

			app.UseSerilogRequestLogging();

			app.UseRouting();

			app.UseAuthentication();
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
		public void ConfigureServices(IServiceCollection services)
		{
			var appSection = Configuration.GetSection("Application");

			services
				.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie();

			services
				.AddDomainLayer()
				.AddApplicationLayer(
					appSection.GetSection("Logging"),
					appSection.GetSection("Caching"),
					appSection.GetSection("Performance"),
					appSection.GetSection("Validation"))
				.AddInfrastructureLayer()
				.AddPersistenceLayer(Configuration.GetConnectionString(DatabaseConnectionName))
				.AddHttpContextAccessor()
				.AddDefaultCorrelationId()
				.AddDistributedMemoryCache()
				.AddMediatR(new[] {typeof(DomainLayer), typeof(ApplicationLayer)}.Select(t => t.Assembly).ToArray())
				.AddSwaggerGen(c =>
				{
					c.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"});
				})
				.AddControllers();
		}
	}
}
