using System;
using System.Globalization;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Cryptography;
using AirSnitch.Infrastructure.Cryptography.Hashing;
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

        public ApiKeyStorageModel ApiKey { get; set; }

        public static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<ClientStorageModel>(cm =>
            {
                cm.MapMember(cm => cm.Id).SetElementName("id");
                cm.MapMember(cm => cm.Name).SetElementName("name");
                cm.MapMember(cm => cm.Description).SetElementName("description");
                cm.MapMember(cm => cm.Type).SetElementName("type");
                cm.MapMember(cm => cm.CreatedOn).SetElementName("createdOn");
                cm.MapMember(cm => cm.ApiKey).SetElementName("apiKey");
            });
        }

        public static ClientStorageModel BuildFromDomainModel(ApiClient client)
        {
            return new ClientStorageModel()
            {
                Id = client.Id,
                Name = client.Name.Value,
                Description = client.Description.Value,
                CreatedOn = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
                Type = client.Type.ToString(),
                ApiKey = ApiKeyStorageModel.BuildFromDomainModel(client.ApiKey)
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
                ApiKey = ApiKeyStorageModel.BuildFromStorageModel(clientStorageModel.ApiKey),
            };
        }
        
        public void SetApiKeyValue(string hashValue)
        {
            this.ApiKey.Value = hashValue;
        }
    }

    internal class ApiKeyStorageModel
    {
        public string IssueDate { get; set; }

        public string ExpirationDate { get; set; }

        public string Value { get; set; }

        public static ApiKeyStorageModel BuildFromDomainModel(ApiKey apiKey)
        {
            return new ApiKeyStorageModel()
            {
                IssueDate = apiKey.IssueDate.ToString(CultureInfo.InvariantCulture),
                ExpirationDate = apiKey.IssueDate.ToString(CultureInfo.InvariantCulture),
                Value = Pbkdf2Hash.Generate(apiKey.Value)
            };
        }
        public static ApiKey BuildFromStorageModel(ApiKeyStorageModel apiKeyStorageModel)
        {
            if (apiKeyStorageModel != null)
            {
                var apiKey =  ApiKey.FromString(apiKeyStorageModel.Value);
                apiKey.IssueDate = apiKey.IssueDate;
                apiKey.ExpirationDate = apiKey.ExpirationDate;
                return apiKey;
            }
            return ApiKey.Empty;
        }

        public static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<ApiKeyStorageModel>(cm =>
            {
                cm.MapMember(cm => cm.Value).SetElementName("value");
                cm.MapMember(cm => cm.IssueDate).SetElementName("issueDate");
                cm.MapMember(cm => cm.ExpirationDate).SetElementName("expiryDate");
            });
        }
    }
}