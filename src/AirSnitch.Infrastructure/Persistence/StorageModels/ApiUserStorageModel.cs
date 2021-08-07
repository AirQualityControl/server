using System.Collections.Generic;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace AirSnitch.Infrastructure.Persistence.StorageModels
{
    internal class ApiUserStorageModel
    {
        public ObjectId Id { get; internal set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string ProfilePicUrl { get; set; }
        public string CreatedOn { get; set; }
        public string Gender { get; set; }

        public string SubscriptionPlanId { get; set; }
        
        public string SubscriptionPlanName { get; set; }

        public string SubscriptionPlanDescription { get; set; }

        public List<SubscriptionPlanQuotaStorageModel> Parameters { get; set; }

        public List<ClientStorageModel> Clients { get; set; }

        public static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<ApiUserStorageModel>(cm =>
            {
                cm.MapMember(cm => cm.Id).SetElementName("_id");
                cm.MapMember(cm => cm.FirstName).SetElementName("firstName");
                cm.MapMember(cm => cm.Email).SetElementName("lastName");
                cm.MapMember(cm => cm.ProfilePicUrl).SetElementName("profilePicUrl");
                cm.MapMember(cm => cm.CreatedOn).SetElementName("createdOn");
                cm.MapMember(cm => cm.Gender).SetElementName("gender");
            });
            
            //SubscriptionPlanQuotaStorageModel.RegisterDbMap();
            //ClientStorageModel.RegisterDbMap();
        }

        public static ApiUserStorageModel CreateFromDomainModel(ApiUser apiUser)
        {
            throw new System.NotImplementedException();
        }

        public ApiUser MapToDomainModel()
        {
            throw new System.NotImplementedException();
        }
    }

    internal class SubscriptionPlanQuotaStorageModel
    {
        public string Period { get; set; }

        public int MaxNumberOfRequests { get; set; }
        
        public static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<ApiUserStorageModel>(cm =>
            {
                cm.MapMember(cm => cm.Id).SetElementName("period");
                cm.MapMember(cm => cm.FirstName).SetElementName("maxNumberOfRequests");
            });
        }
    }
}