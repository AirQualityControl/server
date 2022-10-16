using System.Linq;
using AirSnitch.Domain.Models;
using MongoDB.Bson.Serialization;

namespace AirSnitch.Infrastructure.Persistence.StorageModels
{
    internal class AirPollutionStorageModel
    {
        public ParticleStorageModel[] Particles { get; set; }
        
        public static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<AirPollutionStorageModel>(cm =>
            {
                cm.MapMember(cm => cm.Particles).SetElementName("particles");
            });
        }

        public AirPollution MapToDomainModel()
        {
            var particles = Particles.Select(p => UnknownParticle.CreateInstance(p.Name, p.Value)).ToList();
            return new AirPollution(particles);
        }

        public static AirPollutionStorageModel MapFromDomainModel(AirPollution airPollution)
        {
            return new AirPollutionStorageModel()
            {
                Particles = airPollution.Particles.Select(
                    p => new ParticleStorageModel()
                    {
                        Name = p.ParticleName, Value = p.Value
                    }).ToArray()
            };
        }
    }

    internal class ParticleStorageModel
    {
        public string Name { get; set; }

        public double Value { get; set; }
        
        public static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<ParticleStorageModel>(cm =>
            {
                cm.MapMember(cm => cm.Name).SetElementName("name");
                cm.MapMember(cm => cm.Value).SetElementName("value");
            });
        }
    }
}