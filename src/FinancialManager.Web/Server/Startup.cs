using System.Text.Json.Serialization;
using AutoMapper;
using FinancialManager.Infra.CrossCutting.IoC;
using FinancialManager.Web.Server.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FinancialManager.Web.Server
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllersWithViews()
					.AddJsonOptions(options =>
					{
						options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
						options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.Strict;
					});
			services.AddRazorPages();
			services.AddHttpsRedirection(options =>
			{
				options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
				options.HttpsPort = 5025;
			});
			services.AddAutoMapper(typeof(Startup));
			services.AddServices(Configuration);
			services.ConfigureSwagger();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseWebAssemblyDebugging();

				app.UseCors(policy =>
				{
					policy.WithOrigins("http://localhost:5000", "https://localhost:5001")
						.AllowAnyMethod()
						.AllowAnyHeader();
				});
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseSwaggerConfiguration();
			app.UseHttpsRedirection();
			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();

			app.UseRouting();
			app.UseServicesMiddleware();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapControllers();
				endpoints.MapFallbackToFile("index.html");
			});
		}
	}
}
