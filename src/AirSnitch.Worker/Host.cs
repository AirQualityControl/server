using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AirSnitch.Infrastructure.Abstract.MessageQueue;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AirSnitch.Worker
{
    public class Host : BackgroundService
    {
        private readonly ILogger<Host> _logger;
        private readonly SensorsDataConsumer _sensorsDataConsumer;

        public Host(ILogger<Host> logger, SensorsDataConsumer sensorsDataConsumer)
        {
            _logger = logger;
            _sensorsDataConsumer = sensorsDataConsumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _sensorsDataConsumer.Start(stoppingToken);
        }
    }
}