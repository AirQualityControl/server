namespace AirSnitch.Domain.Models
{
    public interface IAirQualityIndex
    {
        string DisplayName { get; }
        string Description { get; }
        IAirQualityIndexValue Calculate(AirPollution airPollution);
    }
}