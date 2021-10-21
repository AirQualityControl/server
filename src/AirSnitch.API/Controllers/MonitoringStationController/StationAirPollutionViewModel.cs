using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using AirSnitch.Api.Controllers.AirQualityIndexController.ViewModel;
using AirSnitch.Api.Rest.Links;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace AirSnitch.Api.Controllers.MonitoringStationController
{
    public class StationAirPollutionViewModel
    {
        private readonly AirPollution _airPollution;
        private readonly HttpRequest _request;
        private readonly Dictionary<string, object> _resultDictionary;

        public StationAirPollutionViewModel(AirPollution airPollution, HttpRequest request)
        {
            _airPollution = airPollution;
            _request = request;
            _resultDictionary = new Dictionary<string, object>()
            {
                {"particles", null},
                {"measurementTime", null}
            };
        }

        public QueryResult GetResult()
        {
            _resultDictionary["particles"] = new JArray(
                _airPollution.Particles.
                    Select(p => new JObject(
                            new JProperty("name", p.ParticleName), 
                            new JProperty("value",p.Value)
                        )
                    )
            );
            _resultDictionary["measurementTime"] = _airPollution.GetMeasurementsDateTime();
            
            return new QueryResult(
                new List<Dictionary<string, object>>(){_resultDictionary},
                new AirQualityIndexResponseFormatter()
            );
        }

        public void SetStationId(string stationId)
        {
            _resultDictionary.Add("station",
                new MonitoringStationResourceLink(_request, stationId).Value);
        }
    }
    
    internal class ParticleViewModel
    {
        [JsonPropertyName("apiKey")] 
        public string Name { get; set; }

        [JsonPropertyName("apiKey")] 
        public decimal Value { get; set; }
    }
}