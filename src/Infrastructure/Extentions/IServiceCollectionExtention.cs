using AirSnitch.Core.Infrastructure;
using AirSnitch.Core.Infrastructure.Configuration;
using AirSnitch.Core.Infrastructure.EventNotification;
using AirSnitch.Core.Infrastructure.Jobs.Background;
using AirSnitch.Core.Infrastructure.JobStore;
using AirSnitch.Core.Infrastructure.Logging;
using AirSnitch.Core.Infrastructure.Persistence;
using AirSnitch.Core.UseCases.CancelAirMonitoring;
using AirSnitch.Core.UseCases.GetAirPollution;
using AirSnitch.Core.UseCases.StartAirMonitoring;
using AirSnitch.Core.UseCases.UserBlocksAppUseCase;
using AirSnitch.Infrastructure.Configuration;
using AirSnitch.Infrastructure.EventNotifications;
using AirSnitch.Infrastructure.Jobs;
using AirSnitch.Infrastructure.Jobs.Store;
using AirSnitch.Infrastructure.Persistence;
using AirSnitch.Infrastructure.Persistence.Mongo.Mappers;
using AirSnitch.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirSnitch.Infrastructure.Extentions
{
    public static class IServiceCollectionExtention
    {
        public static IServiceCollection AddCoreInfrastructure(this IServiceCollection services)
        {
            
#if DEBUG
            services.AddScoped<IAppConfig, FileBasedAppConfig>();
#else
            services.AddScoped<IAppConfig, EnvironmentVariablesConfig>();
#endif

            services.AddScoped<ISystemComponent, EventNotificationComponent>();
            services.AddScoped<ISystemComponent, MongoDbComponent>();
            services.AddScoped<ISystemComponent, HangfireJobStoreComponent>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAirMonitoringStationRepository, AirMonitoringStationRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IAppComponentConfigurator, CoreAppComponentsConfigurator>();
            //services.AddScoped<IHttpLogger, HttpLogger>();
            services.AddSingleton<ILog, SerilogWrapper>();
            services.AddScoped<IJobExecutor, BackgroundJobExecutor>();
            services.AddScoped<IJobStore, HangfireJobStore>();
            services.AddTransient(typeof(IEventNotificationEmitter<>), typeof(InProcessEventNotificationEmitter<>));
            services.AddTransient(typeof(IEvenNotificationStore<>), typeof(InMemoryEventNotificationStore<>));

            //services.AddScoped<IAirPollutionDataProvider, SaveDniproDataProvider>();!!!!
            services.AddScoped<IUserBlockedAppUseCase, UserBlockedAppUseCase>();
            services.AddScoped<IStartAirMonitoringUseCase, StartAirMonitoringUseCase>();
            services.AddScoped<IGetAirPollutionByGeoLocationUseCase, GetAirPollutionByGeoLocationUseCase>();
            services.AddScoped<ICancelAirMonitoringUseCase, CancelAirMonitoringUseCase>();
            services.AddSingleton<IAirMonitoringStationModelMapper, AirMonitoringStationModelMapper>();

            return services;
        }
    }
}
