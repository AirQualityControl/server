using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
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
            services.AddSingleton(MongoDbClient.Create());
        }
    }
}