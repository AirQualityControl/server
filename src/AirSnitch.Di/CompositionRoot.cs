using AirSnitch.Infrastructure.Abstract.Cryptography;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
using AirSnitch.Infrastructure.Cryptography.Hashing;
using AirSnitch.Infrastructure.Persistence;
using AirSnitch.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AirSnitch.Di
{
    public static class CompositionRoot
    {
        public static void ResolveApplicationDependencies(this IServiceCollection services)
        {
            services.AddTransient<IApiUserRepository, ApiUserRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddScoped<IApiKeyHashAlgorithm, Pbkdf2HashAlgorithm>();
            services.AddTransient<IMonitoringStationRepository, MonitoringStationRepository>();
            services.AddSingleton(MongoDbClient.Create());
        }
    }
}