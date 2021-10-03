using System.Collections.Generic;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence;
using DeclarativeContracts.Precondition;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver.Core.Misc;

namespace AirSnitch.Infrastructure.Persistence.StorageModels
{
    internal class ApiUserStorageModel
    {
        public ObjectId PrimaryKey { get; internal set; }

        public string Id { get; set; }
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
            //SubscriptionPlanQuotaStorageModel.RegisterDbMap();
            
            BsonClassMap.RegisterClassMap<ApiUserStorageModel>(cm =>
            {
                cm.MapMember(cm => cm.PrimaryKey).SetElementName("_id");
                cm.MapMember(cm => cm.Id).SetElementName("id");
                cm.MapMember(cm => cm.FirstName).SetElementName("firstName");
                cm.MapMember(cm => cm.Email).SetElementName("lastName");
                cm.MapMember(cm => cm.ProfilePicUrl).SetElementName("profilePicUrl");
                cm.MapMember(cm => cm.CreatedOn).SetElementName("createdOn");
                cm.MapMember(cm => cm.Gender).SetElementName("gender");
                cm.MapMember(cm => cm.Clients).SetElementName("clients");
            });
        }

        /// <summary>
        ///     Method that convert a valid domain model to storage model
        /// </summary>
        /// Precondition: await a valid api user domain model as input
        /// <param name="apiUser"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static ApiUserStorageModel CreateFromDomainModel(ApiUser apiUser)
        {
            Require.That(apiUser.IsValid);
            
            
            throw new System.NotImplementedException();
        }

        /// <summary>
        ///     Method that maps a fetched storage model to a valid domain model
        /// </summary>
        /// Postcondition: returns always a valid domain model
        /// <returns></returns>
        public ApiUser MapToDomainModel()
        {
            var apiUser = new ApiUser(Id);
            if (Clients != null)
            {
                foreach (var client in Clients)
                {
                    apiUser.AddClient(new ApiClient(client.Id)
                    {
                        Name = new ClientName(client.Name),
                        Description = new ClientDescription(client.Description),
                        CreatedOn = client.CreatedOn,
                        Status = ClientStatus.Active,
                        Type = ClientType.Testing
                    });
                }  
            }

            apiUser.Profile = new ApiUserProfile()
            {
                Name = new UserName(this.FirstName),
                //LastName = new LastName(this.)
                Email = new Email(this.Email),
            };
            
            //TODO: think how to map subscription plan
            apiUser.SetSubscriptionPlan(
                SubscriptionPlan.Basic
            );

            Require.That(apiUser.IsValid);
            return apiUser;
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
                cm.MapMember(cm => cm.PrimaryKey).SetElementName("period");
                cm.MapMember(cm => cm.FirstName).SetElementName("maxNumberOfRequests");
            });
        }
    }
}