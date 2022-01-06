using System.Collections.Generic;
using System.Linq;
using AirSnitch.Api.Controllers.AirQualityIndexController.ViewModel;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace AirSnitch.Api.Controllers.ApiUserController.ViewModels
{
    internal class ApiClientViewModel
    {
        private readonly IReadOnlyCollection<ApiClient> _clients;
        private readonly HttpRequest _request;
        public ApiClientViewModel(IReadOnlyCollection<ApiClient> clients, HttpRequest request)
        {
            _clients = clients;
            _request = request;
            
        }

        public QueryResult GetResult()
        {
            var resultDictionary = new Dictionary<string, object>();
            foreach (var client in _clients)
            {
                resultDictionary["id"] = client.Id;
                resultDictionary["createdOn"] = client.CreatedOn;
                resultDictionary["name"] = client.Name.Value;
                resultDictionary["description"] = client.Description.Value;
                resultDictionary["type"] = client.Type;
            }
            return new QueryResult(
                new List<Dictionary<string, object>>(){resultDictionary},
                new AirQualityIndexResponseFormatter()
            );
        }
    }
}