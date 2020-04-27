using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AutoMapper;

using Plex.Api;
using Plex.Api.Api;
using Plex.Web.Api.Hubs;
using Plex.Web.Api.Services;

namespace Plex.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private readonly string CorsOptions = "CorsOptions";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Setup Client Options
            var clientOptions = new ClientOptions
            {
                ApplicationName = "Plex.Web.Api",
                DeviceName = "Plex.Web.Api",
                ClientId = Guid.Parse("bf7e338d-ab9b-4bf1-be78-d75ac11e9d80")
            };
            
            // Setup CORS
            services.AddCors(options =>
            {
                options.AddPolicy(CorsOptions,
                    builder =>
                    {
                        builder
                            .WithOrigins("http://localhost:8080")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });
            
            // Setup DI
            services.AddLogging();
            services.AddSingleton<IApiService, ApiService>();
            services.AddTransient<IPlexClient, PlexClient>();
            services.AddTransient<IPlexService, PlexService>();
            services.AddTransient<IPlexRequestsHttpClient, PlexRequestsHttpClient>();
            services.AddSingleton<ClientOptions>(clientOptions);

            services.AddSignalR();
            services.AddControllers();
            
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Plex API", Version = "v1" });
            });
            
            // Automapper Registration
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Plex API V1");
                c.RoutePrefix = string.Empty;
            });

            // Enable UseCors with named policy.
            app.UseCors(CorsOptions);
            
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SessionHub>("/hubs/session");
            });
        }
    }
}