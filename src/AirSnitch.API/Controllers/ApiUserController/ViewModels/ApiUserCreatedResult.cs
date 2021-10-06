using System;
using System.Text.Json.Serialization;

namespace AirSnitch.Api.Controllers.ApiUserController.ViewModels
{
    internal class ApiUserCreatedResult
    {
        [JsonPropertyName("clientId")]
        public string UserId { get; set; }
    }
}