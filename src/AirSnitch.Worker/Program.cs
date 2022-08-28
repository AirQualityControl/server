using AirSnitch.Di;
using AirSnitch.Worker.AirPollutionConsumer;
using AirSnitch.Worker.AirPollutionConsumer.Pipeline;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AirSnitch.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;
                    services.ResolveApplicationDependencies(configuration);
                    services.AddSingleton<AirPollutionDataProcessingPipeline>();
                    services.AddSingleton<AirPollutionDataConsumer>();
                    services.AddTransient<ValidateMessageBlock>();
                    services.AddTransient<UpdateStationInfoBlock>();
                    services.AddTransient<AcknowledgeMessageBlock>();
                    services.AddHostedService<Host>();
                });
    }
}