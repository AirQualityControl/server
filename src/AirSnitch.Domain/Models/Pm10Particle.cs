namespace AirSnitch.Domain.Models
{
    public class Pm10Particle : IAirPollutionParticle
    {
        public Pm10Particle(decimal value)
        {
            Value = value;
        }
        public string ParticleName => "PM10";
        public decimal Value { get; }
    }
}