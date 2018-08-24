using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AnimalShelters.Data;
using AnimalShelters.Data.Abstract;
using AnimalShelters.Data.Repositories;
using AnimalShelters.API.ViewModels.Mappings;
using AnimalShelters.API.Core;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace AnimalShelters.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private static string _applicationPath = string.Empty;
        private static string _contentRootPath = string.Empty;
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            _applicationPath = env.WebRootPath;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string sqlConnectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AnimalShelterContext>(options =>
            options.UseSqlServer(Configuration["Data:AnimalSheltersConnection:ConnectionString"],
            b => b.MigrationsAssembly("AnimalShelters.API")));

            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<IAnimalShelterRepository, AnimalShelterRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();

            services.AddAutoMapper();
            services.AddMvc()
                .AddJsonOptions(opts =>
                {
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            // Add MVC to the request pipeline.
            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseExceptionHandler(
              builder =>
              {
                  builder.Run(
                    async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                        }
                    });
              });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                //routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });

            AnimalSheltersDbInitializer.Initialize(app.ApplicationServices);
        }
    }
}
