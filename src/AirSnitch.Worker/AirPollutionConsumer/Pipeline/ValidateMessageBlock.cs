using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.MessageQueue;

namespace AirSnitch.Worker.AirPollutionConsumer.Pipeline
{
    public class ValidateMessageBlock
    {
        public ValidateMessageBlock()
        {
            
        }
        
        public TransformBlock<Message, ValueTuple<Message, MonitoringStation>> Instance = new TransformBlock<Message, ValueTuple<Message, MonitoringStation>>(async receivedMsg =>
        {
            await Task.Delay(300);
            var monitoringStation = new MonitoringStation();
            return (receivedMsg, monitoringStation);
        });
    }
}