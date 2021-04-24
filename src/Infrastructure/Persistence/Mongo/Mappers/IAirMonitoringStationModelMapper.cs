using AirSnitch.Core.Domain.Models;
using AirSnitch.Infrastructure.Persistence.StorageModels;

namespace AirSnitch.Infrastructure.Persistence.Mongo.Mappers
{
    internal interface IAirMonitoringStationModelMapper
    {
        AirMonitoringStation MapToDomainModel(AirMonitoringStationStorageModel storageModel);
        AirMonitoringStationStorageModel MapToDbModel(AirMonitoringStation domainModel);
    }
}