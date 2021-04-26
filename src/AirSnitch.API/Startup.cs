using AirSnitch.Api.Infrastructure.Attributes;
using AirSnitch.Api.Infrastructure.Auth;
using AirSnitch.Api.Infrastructure.Auth.Extensions;
using AirSnitch.Api.Infrastructure.Authorization;
using AirSnitch.Api.Infrastructure.Interfaces;
using AirSnitch.Api.Infrastructure.PathResolver;
using AirSnitch.Api.Infrastructure.PathResolver.Models;
using AirSnitch.Api.Infrastructure.PathResolver.SearchAlgorithms;
using AirSnitch.Api.Infrastructure.Services;
using AirSnitch.Api.Models.Internal;
using AirSnitch.Core.Domain.Models;
using AirSnitch.Infrastructure.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SaveDnipro;
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
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
            })
            .AddApiKeySupport(options => { });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.RequiredUser, policy => policy.Requirements.Add(new UserRequirement()));
                options.AddPolicy(Policies.RequiredAdmin, policy => policy.Requirements.Add(new AdminRequirement()));
            });

            
            services.AddControllers().AddNewtonsoftJson();

            ConfigureIoC(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AirSnitch Api", Version = "v1" });
                c.AddSecurityDefinition(ApiKeyConstants.HeaderName, new OpenApiSecurityScheme
                {
                    Description = "Api key needed to access the endpoints. ApiKey: My_API_Key",
                    In = ParameterLocation.Header,
                    Name = ApiKeyConstants.HeaderName,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = ApiKeyConstants.HeaderName,
                            Type = SecuritySchemeType.ApiKey,
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = ApiKeyConstants.HeaderName },
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        
        protected virtual void ConfigureIoC(IServiceCollection services)
        {
            services.AddTransient<IAirPollutionDataProvider, SaveDniproDataProvider>();//!!!!
            services.AddCoreInfrastructure();

            services.AddSingleton<IAuthorizationHandler, UserAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, AdminAuthorizationHandler>();
            services.AddSingleton<IDummyUserService, DummyUserService>();
            services.AddSingleton<IGetApiKeyQuery, InMemoryGetApiKeyQuery>();

            services.AddSingleton(x => ResourcePathResolver);
            services.AddScoped<ISearchAlgorithm, BFS>();
            services.AddScoped<IAirMonitoringStationService, AirMonitoringStationService>();
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
