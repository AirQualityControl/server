using System.Text.Json.Serialization;

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
    }
}