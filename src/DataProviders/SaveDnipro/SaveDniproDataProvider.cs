using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AirSnitch.Core.Domain.Models;
using AirSnitch.Infrastructure.Network.HTTP;

namespace SaveDnipro
{
    public class SaveDniproDataProvider : IAirPollutionDataProvider
    {
        public AirPollutionDataProviderTag Tag => new AirPollutionDataProviderTag("SaveDnipro");

        private List<KeyValuePair<string, string>> GetRequestParams(string stationId)
        {
            return new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("device_id", stationId)
            };
        }

        public async Task<AirPollution> GetLatestDataAsync(AirMonitoringStation station)
        {
            var requestParams = GetRequestParams(station.Id);

            var requestResult = await Http.Get<AirPollutionResultDto>(
                new Uri("https://www.saveecobot.com/maps/"), 
                new AirMonitoringStationHourlyDataHttpResource(),
                requestParams);

            if (requestResult.IsSuccess && requestResult.Body != null)
            {
                var lastMeasurementKvPair = requestResult.Body.History.First();
                var aqiusValue = lastMeasurementKvPair.Value;

                if (aqiusValue == 0)
                {
                    return AirPollution.Empty;
                }
                var dateTime = DateTime.ParseExact(lastMeasurementKvPair.Key, 
                    "yyyy-MM-dd HH:mm:ss", 
                    CultureInfo.InvariantCulture
                );
                
                return new AirPollution()
                {
                    AqiusValue = aqiusValue,
                    MeasurementDateTime = dateTime,
                    MonitoringStation = station
                };
            }

            return AirPollution.Empty;
        }
    }
}