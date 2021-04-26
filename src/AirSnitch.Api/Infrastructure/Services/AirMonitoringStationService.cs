using AirSnitch.Api.Models;
using AirSnitch.Core.Domain.Models;
using AirSnitch.Core.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.Services
{
    public class AirMonitoringStationService : IAirMonitoringStationService
    {
        private readonly IAirMonitoringStationRepository _airMonitoringStationRepository;
        public AirMonitoringStationService(IAirMonitoringStationRepository airMonitoringStationRepository)
        {
            _airMonitoringStationRepository = airMonitoringStationRepository;
        }

        public async Task<AirMonitoringStationDTO> GetByIdAsync(string stationId)
        {
            var station = await _airMonitoringStationRepository.GetByIdAsync(stationId);
            return new AirMonitoringStationDTO
            {
                IsActive = station.IsActive,
                LocalName = station.LocalName,
                Location = new GeoLocationDTO
                {
                    Latitude = station.Location.Latitude,
                    Longitude = station.Location.Longitude
                },
                TimeZone = station.TimeZone,
                Name = station.Name
            };
        }

        public async Task<(Dictionary<string, AirMonitoringStationDTO>, int)> GetPaginated(int limit, int offset)
        {
            var page = await _airMonitoringStationRepository.GetPage(pageOffset: offset, numberOfItems: limit);
            return (page.Items.ToDictionary(item => item.Id, item => new AirMonitoringStationDTO
            {
                IsActive = item.IsActive,
                LocalName = item.LocalName,
                Location = new GeoLocationDTO
                {
                    Latitude = item.Location.Latitude,
                    Longitude = item.Location.Longitude
                },
                TimeZone = item.TimeZone,
                Name = item.Name
            }), page.TotalNumberOfItems);
        }

        public async Task<CityDTO> GetIncludedCity(string stationId)
        {
            var station = await _airMonitoringStationRepository.GetByIdAsync(stationId);

            return new CityDTO
            {
                State = station.City.State,
                Code = station.City.Code,
                CountryCode = station.City.CountryCode,
                FriendlyName = station.City.FriendlyName
            };
        }

        public async Task<AirPollutionDTO> GetIncludedAirpolution(string stationId)
        {
            var station = await _airMonitoringStationRepository.GetByIdAsync(stationId);
            var airpollution = await station.GetLatestAirPollutionAsync();
            return new AirPollutionDTO
            {
                AqiusValue = airpollution.AqiusValue,
                Humidity = airpollution.Humidity,
                MeasurementDateTime = airpollution.MeasurementDateTime,
                Temperature = airpollution.Temperature,
                WindSpeed = airpollution.WindSpeed,
                Message = $"👉 Індекс якості повітря: *{airpollution.AqiusValue} \n\n* " +
                          $"{airpollution.Analyze().HumanOrientedMessage} \n\n" +
                          $"Дані зібрані зі станції у місті *{airpollution.MonitoringStation.City}*, за адресою *{airpollution.MonitoringStation.Name}*"
            };
        }

        public async Task<DataProviderDTO> GetIncludedDataProvider(string stationId)
        {
            var station = await _airMonitoringStationRepository.GetByIdAsync(stationId);
            return new DataProviderDTO
            {
                DataUpdateInterval = station.DataProvider.DataUpdateInterval,
                Name = station.DataProvider.Name,
                WebSiteUri = station.DataProvider.WebSiteUri
            };
        }


        public async Task<Dictionary<string, object>> GetIncludes(string[] includes, string stationId)
        {
            var station = await _airMonitoringStationRepository.GetByIdAsync(stationId);
            var result = new Dictionary<string, object>();
            Parallel.ForEach(includes, (item) =>
            {
                switch (item)
                {
                    case ControllersRoutes.AirPolution:
                        result.Add(ControllersRoutes.AirPolution, GetIncludedAirpolution(station).Result);
                        break;
                    case ControllersRoutes.City:
                        result.Add(ControllersRoutes.City, GetIncludedCity(station));
                        break;
                    case ControllersRoutes.Dataprovider:
                        result.Add(ControllersRoutes.Dataprovider, GetIncludedDataProvider(station));
                        break;
                    default:
                        throw new ArgumentException($"Incorrect include: {item}");
                }
            });
            return result;
        }

        private CityDTO GetIncludedCity(AirMonitoringStation station)
        {
            return new CityDTO
            {
                State = station.City.State,
                Code = station.City.Code,
                CountryCode = station.City.CountryCode,
                FriendlyName = station.City.FriendlyName
            };
        }

        private async Task<AirPollutionDTO> GetIncludedAirpolution(AirMonitoringStation station)
        {
            var airpollution = await station.GetLatestAirPollutionAsync();
            return new AirPollutionDTO
            {
                AqiusValue = airpollution.AqiusValue,
                Humidity = airpollution.Humidity,
                MeasurementDateTime = airpollution.MeasurementDateTime,
                Temperature = airpollution.Temperature,
                WindSpeed = airpollution.WindSpeed,
                Message = $"👉 Індекс якості повітря: *{airpollution.AqiusValue} \n\n* " +
                          $"{airpollution.Analyze().HumanOrientedMessage} \n\n" +
                          $"Дані зібрані зі станції у місті *{airpollution.MonitoringStation.City}*, за адресою *{airpollution.MonitoringStation.Name}*"
            };
        }

        private DataProviderDTO GetIncludedDataProvider(AirMonitoringStation station)
        {
            return new DataProviderDTO
            {
                DataUpdateInterval = station.DataProvider.DataUpdateInterval,
                Name = station.DataProvider.Name,
                WebSiteUri = station.DataProvider.WebSiteUri
            };
        }

    }
}
