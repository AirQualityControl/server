using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.MessageQueue;

namespace AirSnitch.Worker.AirPollutionConsumer.Pipeline
{
    public class UpdateStationInfoBlock
    {
        public TransformBlock<ValueTuple<Message, MonitoringStation>, Message> Instanse =
            new TransformBlock<ValueTuple<Message, MonitoringStation>, Message>(async tuple =>
            {
                await Task.Delay(300);
                return tuple.Item1;
            });
    }
}