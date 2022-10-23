using System.Threading;
using System.Threading.Tasks;
using AirSnitch.Worker.AirPollutionConsumer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AirSnitch.Worker
{
    public class Host : BackgroundService
    {
        private readonly ILogger<Host> _logger;
        private readonly AirPollutionDataConsumer _airPollutionDataConsumer;

        public Host(ILogger<Host> logger, AirPollutionDataConsumer airPollutionDataConsumer)
        {
            _logger = logger;
            _airPollutionDataConsumer = airPollutionDataConsumer;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _airPollutionDataConsumer.Start(stoppingToken);
            return Task.CompletedTask;
        }
    }
}