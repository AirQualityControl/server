using System;
using System.Collections.Generic;
using System.Linq;
using AirSnitch.Core.Domain.Models;
using AirSnitch.Core.Infrastructure.Logging;
using AirSnitch.Infrastructure.Cache;
using AirSnitch.Infrastructure.Persistence.StorageModels;

namespace AirSnitch.Infrastructure.Persistence.Mongo.Mappers
{
    /// <summary>
    /// Mapper between storage mode and Domain model for AirMonitoringStation
    /// </summary>
    internal class AirMonitoringStationModelMapper : IAirMonitoringStationModelMapper
    {
        private readonly IEnumerable<IAirPollutionDataProvider> _airPollutionDataProviders;
        private readonly ILog _logger;

        public AirMonitoringStationModelMapper(IEnumerable<IAirPollutionDataProvider> airPollutionDataProviders, ILog logger)
        {
            _airPollutionDataProviders = airPollutionDataProviders;
            _logger = logger;
        }

        public AirMonitoringStation MapToDomainModel(AirMonitoringStationStorageModel storageModel)
        {
            if (storageModel != null)
            {
                var dataProvider =
                    _airPollutionDataProviders.Single(p => p.Tag.Equals(new AirPollutionDataProviderTag(storageModel.DataProviderMetaInfo.Tag))); 
                
                var cachedAirPollutionDecorator = new CachedAirPollutionDataProvider<IAirPollutionDataProvider>(
                    dataProvider,
                    MemoryCacheStore<GeoLocation, AirPollution>.Instance,
                    _logger
                    );
                
                return new AirMonitoringStation(cachedAirPollutionDecorator)
                {
                    Id = storageModel.Id,
                    Name = storageModel.Name,
                    City = storageModel.City,
                    IsActive = storageModel.IsActive,
                    IsEmpty = false,
                    LocalName = storageModel.LocalName,
                    Location = storageModel.GeoLocation,
                    TimeZone = storageModel.TimeZone,
                    DataProvider = new AirPollutionDataProvider()
                    {
                        Name = storageModel.DataProviderMetaInfo.Name,
                        DataUpdateInterval = storageModel.DataProviderMetaInfo.DataUpdateInterval,
                        WebSiteUri = new Uri(storageModel.DataProviderMetaInfo.WebSiteUri)
                    }
                };
            }
            throw new ArgumentException("Unable to map storage model");
        }

        public AirMonitoringStationStorageModel MapToDbModel(AirMonitoringStation domainModel)
        {
            throw new NotImplementedException();
        }
    }
}