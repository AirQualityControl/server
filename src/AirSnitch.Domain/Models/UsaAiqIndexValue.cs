using System;
using System.Collections.Generic;

namespace AirSnitch.Domain.Models
{
    public class UsaAiqIndexValue : IAirQualityIndexValue
    {
        private readonly int _calculatedValue;
        private readonly DateTime _measurementDateTime;

        internal UsaAiqIndexValue(int calculatedValue, DateTime measurementDateTime)
        {
            _calculatedValue = calculatedValue;
            _measurementDateTime = measurementDateTime;
        }
        
        public int NumericValue => _calculatedValue;

        public DangerLevel GetDangerLevel()
        {
            return _calculatedValue switch
            {
                > 0 and <= 50 => DangerLevel.Good,
                > 50 and <= 100 => DangerLevel.Moderate,
                > 100 and <= 150 => DangerLevel.UnhealthyForSensitiveGroups,
                > 150 and <= 200 => DangerLevel.Unhealthy,
                > 200 and <= 300 => DangerLevel.VeryUnhealthy,
                > 300 and <= 500 => DangerLevel.Hazardous,
                _ => throw new ArgumentException($"Invalid Index value: {_calculatedValue}")
            };
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

        public DateTime GetMeasurementDateTime()
        {
            return _measurementDateTime;
        }
    }
}