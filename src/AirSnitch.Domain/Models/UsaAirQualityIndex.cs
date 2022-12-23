using System;

namespace AirSnitch.Domain.Models
{
    public class UsaAirQualityIndex : IAirQualityIndex
    {
        private readonly IAirQualityIndexValue _calculatedValue;

        public UsaAirQualityIndex()
        {
            
        }

        internal UsaAirQualityIndex(IAirQualityIndexValue calculatedValue)
        {
            _calculatedValue = calculatedValue;
        }
        
        public string DisplayName => "AQI_US";

        public string Description => "https://www.airnow.gov/aqi/aqi-basics/using-air-quality-index/";

        public IAirQualityIndexValue Calculate(AirPollution airPollution)
        {
            if (_calculatedValue == null)
            {
                return new UsaAiqIndexValue(34, DateTime.UtcNow);
            }
            return _calculatedValue;
        }
    }
}