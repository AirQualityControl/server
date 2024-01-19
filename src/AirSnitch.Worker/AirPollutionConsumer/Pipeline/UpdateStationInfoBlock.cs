using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.MessageQueue;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
using Microsoft.Extensions.Logging;

namespace AirSnitch.Worker.AirPollutionConsumer.Pipeline
{
    public class UpdateStationInfoBlock
    {
        private readonly IMonitoringStationRepository _monitoringStationRepository;
        private readonly ILogger<AirPollutionDataConsumer> _logger;
        public UpdateStationInfoBlock(
            IMonitoringStationRepository monitoringStationRepository, 
            ILogger<AirPollutionDataConsumer> logger)
        {
            _logger = logger;
            _monitoringStationRepository = monitoringStationRepository;
        }
        
        public TransformBlock<ValueTuple<Message, MonitoringStation>, Message> Instance => new TransformBlock<ValueTuple<Message, MonitoringStation>, Message>(Transform);

        private async Task<Message> Transform((Message, MonitoringStation) tuple)
        {
            try
            {
                var monitoringStation = tuple.Item2;
                var existingStation = await _monitoringStationRepository.FindByProviderNameAsync(monitoringStation.DisplayName);
                if (existingStation.IsEmpty)
                {
                    _logger.LogInformation($"new station was added {monitoringStation.DisplayName}");
                    await _monitoringStationRepository.AddAsync(monitoringStation);
                    return tuple.Item1;
                }

                var existingAirPollution = existingStation.GetAirPollution();
                if(existingAirPollution == null)
                {
                    _logger.LogWarning($"Air pollution for existing station {existingStation.DisplayName} is empty");
                    await UpdateStation(monitoringStation, existingStation);
                }

                var existingStationDateTime = existingStation.GetAirPollution()?.GetMeasurementsDateTime();
                var receivedStationDateTime = monitoringStation.GetAirPollution()?.GetMeasurementsDateTime();

                if (receivedStationDateTime > existingStationDateTime)
                {
                    await UpdateStation(monitoringStation, existingStation);
                    _logger.LogInformation($"A new monitoring data for station {monitoringStation.DisplayName} received. New measurement date: {receivedStationDateTime}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured: {ex.Message}, {ex.StackTrace}");
            }
            
            return tuple.Item1;
        }

        private async Task UpdateStation(MonitoringStation monitoringStation, MonitoringStation existingStation)
        {
            monitoringStation.Id = existingStation.Id;
            monitoringStation.PrimaryKey = existingStation.PrimaryKey;
            await _monitoringStationRepository.UpdateAsync(monitoringStation);
        }
    }
}