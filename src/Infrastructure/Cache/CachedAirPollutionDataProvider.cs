using System;
using System.Threading.Tasks;
using AirSnitch.Core.Domain.Models;
using AirSnitch.Core.Infrastructure.Cache;
using AirSnitch.Core.Infrastructure.Geo;
using AirSnitch.Core.Infrastructure.Logging;

namespace AirSnitch.Infrastructure.Cache
{
    public class CachedAirPollutionDataProvider<TProvider> : IAirPollutionDataProvider where TProvider : IAirPollutionDataProvider
    {
        private readonly TProvider _airPollutionDataProvider;
        private readonly ICacheStore<GeoLocation, AirPollution> _cacheStore;
        private readonly ILog _logger;

        public CachedAirPollutionDataProvider(TProvider airPollutionDataProvider, ICacheStore<GeoLocation, AirPollution> cacheStore, ILog logger)
        {
            _airPollutionDataProvider = airPollutionDataProvider;
            _cacheStore = cacheStore;
            _logger = logger;
        }
        
        public AirPollutionDataProviderTag Tag => _airPollutionDataProvider.Tag;

        public async Task<AirPollution> GetLatestDataAsync(AirMonitoringStation station)
        {
            var cachedAirPollution = GetAirPollutionFromGeoLocationCache(station.Location);

            if (!cachedAirPollution.IsEmpty)
            {
                _logger.Info(new
                {
                    Message = $"AirPollution data for station with Id {station.Id} returned from cached",
                    AirPollutionValue = cachedAirPollution
                });
                return await Task.FromResult(cachedAirPollution);
            }
            
            var providerResult = await _airPollutionDataProvider.GetLatestDataAsync(station);
            
            if (providerResult.IsEmpty)
            {
                return await Task.FromResult(providerResult);
            }

            _cacheStore.SetValue(
                station.Location, 
                providerResult,
                new CacheEntryPolicy
                {
                    AbsoluteExpirationTime = TimeSpan.FromMinutes(30)
                }
            );
            _logger.Info("Data from air pollution data provider successfully saved in cache");
            return providerResult;
        }
        
        private AirPollution GetAirPollutionFromGeoLocationCache(GeoLocation geoLocation)
        {
            var boundingBox = BoundingBox.Create(point: geoLocation, halfSideInKm: 1);

            var cachedGeolocationEnumeration = _cacheStore.Keys();

            var resultPoint = boundingBox.GetFirstPointFromEnumerationInsideBoundingBox(cachedGeolocationEnumeration);
            
            if (resultPoint != null)
            {
                _cacheStore.TryGetValue(resultPoint, out AirPollution cachedValue);
                return cachedValue ?? AirPollution.Empty;
            }
            return AirPollution.Empty;
        }
        
    }
}