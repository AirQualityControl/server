using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AirSnitch.Infrastructure.Abstract.MessageQueue;
using AirSnitch.Worker.AirPollutionConsumer.Pipeline;
using Microsoft.Extensions.Logging;

namespace AirSnitch.Worker.AirPollutionConsumer
{
    public class AirPollutionDataConsumer
    {
        private readonly ILogger<AirPollutionDataConsumer> _logger;
        private readonly IDistributedMessageQueue _sensorsDataQueue;
        private readonly AirPollutionDataProcessingPipeline _airPollutionDataProcessingPipeline;

        public AirPollutionDataConsumer(
            ILogger<AirPollutionDataConsumer> logger, 
            IDistributedMessageQueue sensorsDataQueue, 
            AirPollutionDataProcessingPipeline airPollutionDataProcessingPipeline)
        {
            _logger = logger;
            _sensorsDataQueue = sensorsDataQueue;
            _airPollutionDataProcessingPipeline = airPollutionDataProcessingPipeline;
        }
        
        public void Start(CancellationToken token)
        {
            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    var messages = await _sensorsDataQueue.GetMessageBatchAsync(batchSize:10);
                    if (messages.Any())
                    {
                        foreach (var msg in messages)
                        {
                            _airPollutionDataProcessingPipeline.PostMessage(msg);
                        }
                        _logger.LogInformation($"Batch of messages ({messages.Count}) was pasted to pipeline, {DateTime.UtcNow}");
                    }
                    else
                    {
                        await Task.Delay(60000, token);
                    }
                }
            }, token);
        }
    }
}