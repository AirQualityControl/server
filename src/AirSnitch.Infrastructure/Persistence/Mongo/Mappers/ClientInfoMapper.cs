using AirSnitch.Infrastructure.Persistence.StorageModels;
using MongoDB.Bson.Serialization;

namespace AirSnitch.Infrastructure.Persistence.Mongo.Mappers
{
    /// <summary>
    /// Mapper between storage model of type <see cref="ClientInfoDbModel"/> model and mongoDb document
    /// </summary>
    internal static class ClientInfoMapper
    {
        public static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<ClientInfoDbModel>(cm =>
            {
                cm.MapMember(c => c.Name).SetElementName("name");
            });
        }
    }
}