using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AirSnitch.Infrastructure.Abstract.MessageQueue;
using Microsoft.Extensions.Logging;

namespace AirSnitch.Worker
{
    public class SensorsDataConsumer
    {
        private readonly ILogger<SensorsDataConsumer> _logger;
        private readonly IDistributedMessageQueue _sensorsDataQueue;

        public SensorsDataConsumer(ILogger<SensorsDataConsumer> logger, IDistributedMessageQueue sensorsDataQueue)
        {
            _logger = logger;
            _sensorsDataQueue = sensorsDataQueue;
        }
        
        public void Start(CancellationToken token)
        {
            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    var messageCount = await _sensorsDataQueue.GetApproximateMessageCount();
                    while (messageCount > 0)
                    {
                        var messages = await _sensorsDataQueue.GetMessageBatchAsync(batchSize:10);
                        if (messages.Any())
                        {
                            foreach (var msg in messages)
                            {
                                Console.WriteLine(msg.Body);
                            }
                            await _sensorsDataQueue.DeleteMessageBatchAsync(messages);
                            messageCount -= 10;
                            _logger.LogInformation($"batch was successfully processed and deleted! {messageCount} messages left");
                        }
                        else
                        {
                            messageCount = 0;
                        }
                    }
                    await Task.Delay(300000, token);
                }
            });
        }
    }
}