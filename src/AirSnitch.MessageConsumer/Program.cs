using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirSnitch.Di;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AirSnitch.MessageConsumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IConfiguration Configuration { get; }
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.ResolveApplicationDependencies(Configuration);
                    services.AddHostedService<Worker>();
                });
    }
}