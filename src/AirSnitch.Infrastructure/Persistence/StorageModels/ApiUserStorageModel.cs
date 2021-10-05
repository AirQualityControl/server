using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AirSnitch.Domain.Models;
using DeclarativeContracts.Precondition;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace AirSnitch.Infrastructure.Persistence.StorageModels
{
    internal class ApiUserStorageModel
    {
        public ObjectId PrimaryKey { get; internal set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfilePicUrl { get; set; }
        public string CreatedOn { get; set; }
        public string Gender { get; set; }
        public SubscriptionPlanStorageModel Plan { get; set; }
        public List<ClientStorageModel> Clients { get; set; }
        public static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<ApiUserStorageModel>(cm =>
            {
                cm.MapMember(cm => cm.PrimaryKey).SetElementName("_id");
                cm.MapMember(cm => cm.Id).SetElementName("id");
                cm.MapMember(cm => cm.FirstName).SetElementName("firstName");
                cm.MapMember(cm => cm.LastName).SetElementName("lastName");
                cm.MapMember(cm => cm.Email).SetElementName("email");
                cm.MapMember(cm => cm.ProfilePicUrl).SetElementName("profilePicUrl");
                cm.MapMember(cm => cm.CreatedOn).SetElementName("createdOn");
                cm.MapMember(cm => cm.Gender).SetElementName("gender");
                cm.MapMember(cm => cm.Clients).SetElementName("clients");
                cm.MapMember(cm => cm.Plan).SetElementName("subscriptionPlan");
            });
        }

        /// <summary>
        ///     Method that convert a valid domain model to storage model
        /// </summary>
        /// Precondition: await a valid api user domain model as input
        /// <param name="apiUser"></param>
        /// <returns></returns>
        public static ApiUserStorageModel CreateFromDomainModel(ApiUser apiUser)
        {
            Require.That(apiUser.IsValid);

            return new ApiUserStorageModel()
            {
                Id = apiUser.Id,
                PrimaryKey = ObjectId.Parse(apiUser.PrimaryKey),
                FirstName = apiUser.Profile.Name.Value,
                LastName = apiUser.Profile.LastName.Value,
                Gender = apiUser.Profile.Gender.ToString(),
                Email = apiUser.Profile.Email.Value,
                ProfilePicUrl = apiUser.Profile.ProfilePic.Value,
                CreatedOn = apiUser.Profile.CreatedOn.ToString(CultureInfo.InvariantCulture),
                Clients = apiUser.Clients.Select(ClientStorageModel.BuildFromDomainModel).ToList(),
                Plan = SubscriptionPlanStorageModel.BuildFromDomainModel(apiUser.SubscriptionPlan),
            };
        }

        /// <summary>
        ///     Method that maps a fetched storage model to a valid domain model
        /// </summary>
        /// Postcondition: returns always a valid domain model
        /// <returns></returns>
        public ApiUser MapToDomainModel()
        {
            var apiUser = new ApiUser(Id);
            apiUser.PrimaryKey = PrimaryKey.ToString();
            if (Clients != null)
            {
                foreach (var client in Clients)
                {
                    apiUser.AddClient(ClientStorageModel.BuildFromStorageModel(client));
                }  
            }

            apiUser.Profile = new ApiUserProfile()
            {
                Name = new UserName(this.FirstName),
                LastName = new LastName(this.LastName),
                Email = new Email(this.Email),
                ProfilePic = new ProfilePicture(this.ProfilePicUrl),
                Gender = Domain.Models.Gender.FromString(this.Gender),
                CreatedOn = DateTime.Parse(this.CreatedOn),
            };
            
            apiUser.SetSubscriptionPlan(SubscriptionPlanStorageModel.BuildFromStorageModel(this.Plan));

            Require.That(apiUser.IsValid);
            return apiUser;
        }
    }


    internal class SubscriptionPlanStorageModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ExpirationDate { get; set; }

        public SubscriptionPlanQuotaStorageModel Quota { get; set; }

        public static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<SubscriptionPlanStorageModel>(cm =>
            {
                cm.MapMember(p => p.Id).SetElementName("id");
                cm.MapMember(p => p.Name).SetElementName("name");
                cm.MapMember(p => p.Description).SetElementName("description");
                cm.MapMember(p => p.ExpirationDate).SetElementName("expirationDate");
                //cm.MapMember(p => p.Quota).SetElementName("parameters");
            });
        }

        public static SubscriptionPlanStorageModel BuildFromDomainModel(SubscriptionPlan apiUserSubscriptionPlan)
        {
            return new SubscriptionPlanStorageModel()
            {
                Id = apiUserSubscriptionPlan.Id,
                Name = apiUserSubscriptionPlan.Name,
                Description = apiUserSubscriptionPlan.Description,
                //Quota = SubscriptionPlanQuotaStorageModel.BuildFromDomainModel(apiUserSubscriptionPlan.RequestQuota),
                ExpirationDate = apiUserSubscriptionPlan.ExpirationDate.ToString(CultureInfo.InvariantCulture)
            };
        }

        public static SubscriptionPlan BuildFromStorageModel(SubscriptionPlanStorageModel subscriptionPlanStorageModel)
        {
            return new SubscriptionPlan()
            {
                Id = subscriptionPlanStorageModel.Id,
                Description = subscriptionPlanStorageModel.Description,
                Name = subscriptionPlanStorageModel.Name,
                ExpirationDate = DateTime.Parse(subscriptionPlanStorageModel.ExpirationDate),
                //RequestQuota = new RequestQuota()
                //{
                    //Period = Period.Month,
                    //MaxNumberOfRequests = subscriptionPlanStorageModel.Quota.MaxNumberOfRequests
                //}
            };
        }
    }

    internal class SubscriptionPlanQuotaStorageModel
    {
        public string Period { get; set; }

        public int MaxNumberOfRequests { get; set; }
        
        public static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<SubscriptionPlanQuotaStorageModel>(cm =>
            {
                cm.MapMember(cm => cm.Period).SetElementName("period");
                cm.MapMember(cm => cm.MaxNumberOfRequests).SetElementName("maxNumberOfRequests");
            });
        }

        public static SubscriptionPlanQuotaStorageModel BuildFromDomainModel(RequestQuota requestQuota)
        {
            return new SubscriptionPlanQuotaStorageModel()
            {
                Period = requestQuota.Period.ToString(),
                MaxNumberOfRequests = requestQuota.MaxNumberOfRequests
            };
        }
    }
}