using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Api.Controllers.MonitoringStationController
{
    public class StationAirQualityIndexViewModel
    {
        public StationAirQualityIndexViewModel(UsaAirQualityIndex usaAqi, IAirQualityIndexValue airQualityIndexValue)
        {
            
        }

        public QueryResult GetResult()
        {
            throw new System.NotImplementedException();
        }
    }
}