using System;
using System.Collections.Generic;
using AirSnitch.Api.Rest.Links;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using Microsoft.AspNetCore.Http;

namespace AirSnitch.Api.Controllers.AirQualityIndexController.ViewModel
{
    public class AirQualityIndexViewModel
    {
        private readonly IAirQualityIndex _index;
        private readonly IAirQualityIndexValue _value;
        private readonly Dictionary<string, object> _returnValuesDictionary;
        private readonly HttpRequest _request;

        public AirQualityIndexViewModel(IAirQualityIndex index, IAirQualityIndexValue indexValue, HttpRequest request)
        {
            _index = index;
            _value = indexValue;
            _request = request;
            _returnValuesDictionary = new Dictionary<string, object>()
            {
                {"name", null},
                {"value", null},
                {"level", null}
            };
        }

        public void SetStationId(string stationId)
        {
            _returnValuesDictionary.Add("station",
                new MonitoringStationResourceLink(_request, stationId).Value);
        }
        
        public void SetMeasurementDateTime(DateTime measurementDateTime)
        {
            _returnValuesDictionary.Add("measurementTime", measurementDateTime);
        }
        
        public QueryResult GetResult()
        {
            _returnValuesDictionary["name"] = _index.DisplayName;
            _returnValuesDictionary["value"] = _value.NumericValue;
            _returnValuesDictionary["level"] = _value.GetDangerLevel().ToString();
            
            return new QueryResult(
                new List<Dictionary<string, object>>(){_returnValuesDictionary},
                new AirQualityIndexResponseFormatter()
            );
        }

        
    }
}