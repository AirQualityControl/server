using System.Collections.Generic;
using AirSnitch.Api.Controllers.AirQualityIndexController.ViewModel;
using AirSnitch.Api.Extensions;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using Newtonsoft.Json.Linq;

namespace AirSnitch.Api.Controllers.AirQualityIndexController
{
    public class AirQualityIndexViewModel
    {
        private readonly IAirQualityIndex _index;
        private readonly IAirQualityIndexValue _value;

        public AirQualityIndexViewModel(IAirQualityIndex index, IAirQualityIndexValue indexValue)
        {
            _index = index;
            _value = indexValue;
        }

        public QueryResult GetResult()
        {
            var resultDictionary = new Dictionary<string, object>()
            {
                {"name", _index.DisplayName},
                {"details", _index.Description},
                {"value", _value.NumericValue},
                {"level", _value.GetDangerLevel().ToString()},
                {"description", _value.GetDescription().Text},
                {"advice", _value.GetAdvice().Text}
            };

            return new QueryResult(
                new List<Dictionary<string, object>>(){resultDictionary},
                new AirQualityIndexResponseFormatter(),
                new PageOptions()
            );
        }
    }
}