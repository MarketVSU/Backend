using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using ClothingStore.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ClothingStore.Configuration.AuthTokenConfig;
using ClothingStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ClothingStore
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.RequireHttpsMetadata = false;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,

						ValidIssuer = AuthOptions.ISSUER,

						ValidateAudience = true,

						ValidAudience = AuthOptions.AUDIENCE,

						ValidateLifetime = true,

						IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),

						ValidateIssuerSigningKey = true
					};
				}
				);

			string connection = Configuration.GetConnectionString("DefaultConnection");

			services.AddDbContext<ApplicationContext>(options =>
				options.UseSqlServer(connection));

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo 
				{ 
					Title = "Clothing Store",
					Version = "v1", 
					Description = "Project was created for VSU",
					Contact = new OpenApiContact
					{
						Name = "Latynin Gennadiy, Armen Tovmasyan, Lesnikh Ivan"
					}
				});

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});

			services.AddScoped<DataProvider>();
			services.AddScoped<UserDataProvider>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostEnvironment env)
		{
			app.UseStaticFiles();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseSwagger(c =>
			{
				c.RouteTemplate = "swagger/{documentName}/swagger.json";
			});

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
				c.RoutePrefix = "swagger";
			});

			app.UseRouting();

			app.UseEndpoints(end =>
			{
				end.MapDefaultControllerRoute();
				end.MapControllerRoute("api", "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
