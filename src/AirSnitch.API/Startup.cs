using AirSnitch.Api.Infrastructure.Attributes;
using AirSnitch.Api.Infrastructure.Interfaces;
using AirSnitch.Api.Infrastructure.PathResolver;
using AirSnitch.Api.Infrastructure.PathResolver.Models;
using AirSnitch.Api.Infrastructure.PathResolver.SearchAlgorithms;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AirSnitch.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ResourcePathResolver = new ResoursePathResolver(new BFS(), GraphBuilder.BuildResourseGraph());
        }


        public IConfiguration Configuration { get; }


        protected virtual IResoursePathResolver ResourcePathResolver { get; set; }
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllers().AddNewtonsoftJson();
            ConfigureIoC(services);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AirSnitch Api", Version = "v1" });
            });
        }

        

        protected virtual void ConfigureIoC(IServiceCollection services)
        {
            services.AddSingleton(x => ResourcePathResolver);
            services.AddScoped<ISearchAlgorithm, BFS>();
        }




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AirSnitch Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
