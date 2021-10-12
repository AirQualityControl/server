using System;
using System.Text.Json.Serialization;

namespace AirSnitch.Api.Controllers.ClientController.ViewModel
{
    public class ApiKeyCreatedResult
    {
        [JsonPropertyName("apiKey")] 
        public string Value { get; set; }

        [JsonPropertyName("issueDate")] 
        public DateTime IssueDate { get; set; }

        [JsonPropertyName("expiryDate")] 
        public DateTime ExpiryDate { get; set; }
    }
}