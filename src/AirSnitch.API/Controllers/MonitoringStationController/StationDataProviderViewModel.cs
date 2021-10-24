using System.Collections.Generic;
using AirSnitch.Api.Controllers.AirQualityIndexController.ViewModel;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Api.Controllers.MonitoringStationController
{
    public class StationDataProviderViewModel
    {
        private readonly MonitoringStationOwner _stationOwner;
        private readonly Dictionary<string, object> _returnValuesDictionary;

        public StationDataProviderViewModel(MonitoringStationOwner stationOwner)
        {
            _stationOwner = stationOwner;
            _returnValuesDictionary = new Dictionary<string, object>()
            {
                {"id", null},
                {"name", null},
                {"web-site", null}
            };
        }

        public QueryResult GetResult()
        {
            _returnValuesDictionary["id"] = _stationOwner.Id;
            _returnValuesDictionary["name"] = _stationOwner.Name;
            _returnValuesDictionary["web-site"] = _stationOwner.WebSite;

            return new QueryResult(
                new List<Dictionary<string, object>>(){_returnValuesDictionary},
                new AirQualityIndexResponseFormatter()
            );
        }
    }
}