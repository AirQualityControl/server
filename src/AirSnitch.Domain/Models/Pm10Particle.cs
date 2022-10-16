namespace AirSnitch.Domain.Models
{
    public class Pm10Particle : IAirPollutionParticle
    {
        public Pm10Particle(double value)
        {
            Value = value;
        }
        public string ParticleName => "PM10";
        public double Value { get; }
    }
}