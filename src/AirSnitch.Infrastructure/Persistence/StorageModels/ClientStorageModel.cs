using System;
using MongoDB.Bson.Serialization;

namespace AirSnitch.Infrastructure.Persistence.StorageModels
{
    internal class ClientStorageModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Type { get; set; }
        
        public static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<ClientStorageModel>(cm =>
            {
                cm.MapMember(cm => cm.Id).SetElementName("id");
                cm.MapMember(cm => cm.Name).SetElementName("name");
                cm.MapMember(cm => cm.Description).SetElementName("description");
                cm.MapMember(cm => cm.Type).SetElementName("type");
                cm.MapMember(cm => cm.CreatedOn).SetElementName("createdOn");
            });
        }
    }
}