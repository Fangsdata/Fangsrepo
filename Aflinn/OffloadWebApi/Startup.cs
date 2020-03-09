using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OffloadWebApi.Controllers;
using OffloadWebApi.Services;
using OffloadWebApi.Repository;

namespace OffloadWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(
                options => options.AddPolicy(
                    "AllowCors",
                    builder =>
                    {
                        builder

                            // .WithOrigins("http://localhost:4456") //AllowSpecificOrigins;
                            // .WithOrigins("http://localhost:4456", "http://localhost:4457") //AllowMultipleOrigins;
                            .AllowAnyOrigin() // AllowAllOrigins;

                                              // .WithMethods("GET") //AllowSpecificMethods;
                                              // .WithMethods("GET", "PUT") //AllowSpecificMethods;
                                              // .WithMethods("GET", "PUT", "POST") //AllowSpecificMethods;
                            .WithMethods("GET") // AllowSpecificMethods;

                                                                         // .AllowAnyMethod() //AllowAllMethods;
                                                                         // .WithHeaders("Accept", "Content-type", "Origin", "X-Custom-Header"); //AllowSpecificHeaders;
                            .AllowAnyHeader(); // AllowAllHeaders;
        }));
            services.AddControllers();

            services.AddScoped<IOffloadRepo, OffloadRepoTest>();
            
          // services.AddScoped<IOffloadRepo>(_ => new OffloadOldDbRepo(this.Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddScoped<IOffloadService, OffloadService>();
            services.AddScoped<IBoatService, BoatService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowCors");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
