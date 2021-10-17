namespace AirSnitch.Domain.Models
{
    public interface IAirQualityIndexValue
    {
        int NumericValue { get; }
        DangerLevel GetDangerLevel();
        AirQualityDescription GetDescription();
        AirPollutionAdvice GetAdvice();
    }
}