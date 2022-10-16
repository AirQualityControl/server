using System;
using System.Collections.Generic;

namespace AirSnitch.Domain.Models
{
    public class AirPollution
    {
        private readonly IReadOnlyCollection<IAirPollutionParticle> _particles;
        private IAirQualityIndex _airQualityIndex;
        private IAirQualityIndexValue _calculatedAirQualityIndex;

        public AirPollution(IReadOnlyCollection<IAirPollutionParticle> particles)
        {
            _particles = particles;
        }

        public IReadOnlyCollection<IAirPollutionParticle> Particles => _particles;

        /// <summary>
        ///     Returns a measurement date and time
        /// </summary>
        /// <returns></returns>
        public DateTime GetMeasurementsDateTime()
        {
            return DateTime.UtcNow;
        }

        /// <summary>
        ///     Set already calculated air quality index data alongside with a particle collection.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="indexValue"></param>
        internal void SetAirQualityIndex(IAirQualityIndex index, IAirQualityIndexValue value)
        {
            _airQualityIndex = index;
            _calculatedAirQualityIndex = value;
        }

        public IAirQualityIndexValue GetAirQualityIndexValue()
        {
            return _calculatedAirQualityIndex;
        }
    }

    public interface IAirPollutionParticle
    {
        string ParticleName { get; }
        double Value { get; }
    }

    //???Replace with AirPollution.TryParseParticle(name, value, out IAirPollutionParticle);
    public class UnknownParticle
    {
        public static IAirPollutionParticle CreateInstance(string name, double value)
        {
            if (name == "PM2.5")
            {
                return new Pm25Particle(value);
            }
            return new Pm10Particle(value);
        }
    }

    public class Pm25Particle : IAirPollutionParticle
    {
        public Pm25Particle(double value)
        {
            Value = value;
        }
        public string ParticleName => "PM2.5";
        public double Value { get; }
    }
}