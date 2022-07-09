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

            var station = await _monitoringStationRepository.FindByIdAsync(monitoringStation.Id);

            if (station.IsEmpty)
            {
                await _monitoringStationRepository.AddAsync(monitoringStation);
            }
            await _monitoringStationRepository.UpdateAsync(monitoringStation);
            return tuple.Item1;
        }
    }
}