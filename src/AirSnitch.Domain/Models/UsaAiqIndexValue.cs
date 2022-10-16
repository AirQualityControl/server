namespace AirSnitch.Domain.Models
{
    public class UsaAiqIndexValue : IAirQualityIndexValue
    {
        private readonly int _calculatedValue;

        internal UsaAiqIndexValue(int calculatedValue)
        {
            _calculatedValue = calculatedValue;
        }
        
        public int NumericValue => _calculatedValue;

        public DangerLevel GetDangerLevel()
        {
            return DangerLevel.Good;
        }

        public AirQualityDescription GetDescription()
        {
            return new AirQualityDescription()
            {
                Text = "Great air here today!"
            };
        }

        public AirPollutionAdvice GetAdvice()
        {
            return new AirPollutionAdvice()
            {
                Text = "Don't hesitate to go out today"
            };
        }
    }
}