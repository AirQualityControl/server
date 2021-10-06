using System;
using System.Globalization;
using AirSnitch.Domain.Models;
using MongoDB.Bson.Serialization;

namespace AirSnitch.Infrastructure.Persistence.StorageModels
{
    internal class ClientStorageModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CreatedOn { get; set; }

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

        public static ClientStorageModel BuildFromDomainModel(ApiClient client)
        {
            return new ClientStorageModel()
            {
                Id = client.Id,
                Name = client.Name.Value,
                Description = client.Description.Value,
                CreatedOn = client.CreatedOn.ToString(CultureInfo.InvariantCulture),
                Type = client.Type.ToString()
            };
        }

        public static ApiClient BuildFromStorageModel(ClientStorageModel clientStorageModel)
        {
            return new ApiClient(clientStorageModel.Id)
            {
                Name = new ClientName(clientStorageModel.Name),
                Description = new ClientDescription(clientStorageModel.Description),
                CreatedOn = DateTime.Parse(clientStorageModel.CreatedOn),
                Type = ClientType.Testing,
            };
        }
    }
}