using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.MessageQueue;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;

namespace AirSnitch.Worker.AirPollutionConsumer.Pipeline
{
    public class UpdateStationInfoBlock
    {
        private readonly IMonitoringStationRepository _monitoringStationRepository;
        public UpdateStationInfoBlock(IMonitoringStationRepository monitoringStationRepository)
        {
            _monitoringStationRepository = monitoringStationRepository;
        }
        
        public TransformBlock<ValueTuple<Message, MonitoringStation>, Message> Instance => new TransformBlock<ValueTuple<Message, MonitoringStation>, Message>(Transform);

        private async Task<Message> Transform((Message, MonitoringStation) tuple)
        {
            var monitoringStation = tuple.Item2;
            var existingStation = await _monitoringStationRepository.FindByProviderNameAsync(monitoringStation.DisplayName);
            if (existingStation.IsEmpty)
            {
                await _monitoringStationRepository.AddAsync(monitoringStation);
                return tuple.Item1;
            }
            monitoringStation.PrimaryKey = existingStation.PrimaryKey;
            await _monitoringStationRepository.UpdateAsync(monitoringStation);
            return tuple.Item1;
        }
    }
}