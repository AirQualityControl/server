using AirSnitch.Api.Models;
using AirSnitch.Api.Models.Internal;
using AirSnitch.Core.Domain.Models;
using AirSnitch.Core.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IApiUserRepository _userRepository;
        private readonly IAirMonitoringStationRepository _airMonitoringStationRepository;
        public UserService(IApiUserRepository cityRepository, IAirMonitoringStationRepository airMonitoringStationRepository)
        {
            _airMonitoringStationRepository = airMonitoringStationRepository;
            _userRepository = cityRepository;
        }
        public async Task<UserDTO> GetByIdAsync(string stationId)
        {
            var station = await _userRepository.GetByIdAsync(stationId);
            return station;
        }

        public async Task<(Dictionary<string, UserDTO>, int)> GetPaginated(int limit, int offset)
        {
            var page = await _userRepository.GetPage(pageOffset: offset, numberOfItems: limit);
            return (page.Items, page.TotalNumberOfItems);
        }

        public async Task<CityDTO> GetIncludedCity(string userId)
        {
            var stationId = await _userRepository.GetRelatedStationId(userId);
            var station = await _airMonitoringStationRepository.GetByIdAsync(stationId);

            return new CityDTO
            {
                State = station.City.State,
                Code = station.City.Code,
                CountryCode = station.City.CountryCode,
                FriendlyName = station.City.FriendlyName
            };
        }

        public async Task<AirPollutionDTO> GetIncludedAirpolution(string userId)
        {
            var stationId = await _userRepository.GetRelatedStationId(userId);
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

        public async Task<DataProviderDTO> GetIncludedDataProvider(string userId)
        {
            var stationId = await _userRepository.GetRelatedStationId(userId);
            var station = await _airMonitoringStationRepository.GetByIdAsync(stationId);
            return new DataProviderDTO
            {
                DataUpdateInterval = station.DataProvider.DataUpdateInterval,
                Name = station.DataProvider.Name,
                WebSiteUri = station.DataProvider.WebSiteUri
            };
        }

        public async Task<AirMonitoringStationDTO> GetIncludedAirMonitoringStation(string userId)
        {
            var stationId = await _userRepository.GetRelatedStationId(userId);
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

        public async Task<Dictionary<string, object>> GetIncludes(string[] includes, string userId)
        {
            var stationId = await _userRepository.GetRelatedStationId(userId);
            var station = await _airMonitoringStationRepository.GetByIdAsync(stationId);
            var result = new Dictionary<string, object>();
            Parallel.ForEach(includes, async (item) =>
            {
                switch (item)
                {
                    case ControllersRoutes.AirmonitoringStation:
                        result.Add(ControllersRoutes.AirmonitoringStation, GetIncludedAirMonitoringStation(station));
                        break;
                    case ControllersRoutes.AirPolution:
                        result.Add(ControllersRoutes.AirPolution, await GetIncludedAirpolution(station));
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

        private AirMonitoringStationDTO GetIncludedAirMonitoringStation(AirMonitoringStation station)
        {
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

    }
}
