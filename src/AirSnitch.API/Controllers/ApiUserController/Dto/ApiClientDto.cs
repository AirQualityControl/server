using System.Text.Json.Serialization;
using AirSnitch.Domain.Models;

namespace AirSnitch.Api.Controllers.ApiUserController.Dto
{
    public class ApiClientDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        public ApiClient CreateApiClient()
        {
            var apiClient = new ApiClient()
            {
                Name = new ClientName(Name),
                Description = new ClientDescription(Description),
                Type = ClientType.Production
            };
            return apiClient;
        }
    }
}