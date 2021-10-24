namespace AirSnitch.Domain.Models
{
    public class UsaAirQualityIndex : IAirQualityIndex
    {
        public string DisplayName => "AQI_US";

        public string Description => "https://www.airnow.gov/aqi/aqi-basics/using-air-quality-index/";

        public IAirQualityIndexValue Calculate(AirPollution airPollution)
        {
            return new UsaAiqIndexValue(34.5m);
        }
    }
}