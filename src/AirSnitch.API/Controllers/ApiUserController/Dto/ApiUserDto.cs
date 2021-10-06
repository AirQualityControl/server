using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using AirSnitch.Domain.Models;
using Newtonsoft.Json;

namespace AirSnitch.Api.Controllers.ApiUserController.Dto
{
    /// <summary>
    /// Data transfer model that represent an api user
    /// </summary>
    public class ApiUserDto
    {
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("profilePicUrl")]
        public string ProfilePicUrl { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("subscriptionPlanCode")]
        public string SubscriptionPlanCode { get; set; }

        [JsonPropertyName("clients")]
        public List<ApiClientDto> Clients { get; set; }

        public Domain.Models.ApiUser CreateApiUser()
        {
            var apiUser = new Domain.Models.ApiUser()
            {
                Profile = new ApiUserProfile()
                {
                    Name = new UserName(FirstName),
                    LastName = new LastName(LastName),
                    Email = new Email(Email),
                    ProfilePic = new ProfilePicture(ProfilePicUrl),
                    Gender = Domain.Models.Gender.FromString(Gender)
                },
            };

            var subscriptionPlan = SubscriptionPlan.CreateFromCode(SubscriptionPlanCode);
            apiUser.SetSubscriptionPlan(subscriptionPlan);

            foreach (var clientDto in Clients)
            {
                var apiClient = new ApiClient()
                {
                    Name = new ClientName(clientDto.Name),
                    Description = new ClientDescription(clientDto.Description),
                    Type = ClientType.Testing,
                };
                
                apiClient.GenerateId();
                
                apiUser.AddClient(apiClient);
            }
            
            apiUser.GenerateId();
            return apiUser;
        }
        public Domain.Models.ApiUser CreateApiUser(string id)
        {
            var apiUser = new Domain.Models.ApiUser(id)
            {
                Profile = new ApiUserProfile()
                {
                    Name = new UserName(FirstName),
                    LastName = new LastName(LastName),
                    Email = new Email(Email),
                    ProfilePic = new ProfilePicture(ProfilePicUrl),
                    Gender = Domain.Models.Gender.FromString(Gender)
                },
            };

            foreach (var clientDto in Clients)
            {
                var apiClient = new ApiClient()
                {
                    Name = new ClientName(clientDto.Name),
                    Description = new ClientDescription(clientDto.Description),
                    Type = ClientType.Testing,
                };
                
                apiClient.GenerateId();
                
                apiUser.AddClient(apiClient);
            }
            
            var subscriptionPlan = SubscriptionPlan.CreateFromCode(SubscriptionPlanCode);
            apiUser.SetSubscriptionPlan(subscriptionPlan);
            return apiUser;
        }
    }
}