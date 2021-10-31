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
        private readonly Dictionary<string, object> _resultDictionary;

        public ApiClientViewModel(IReadOnlyCollection<ApiClient> clients, HttpRequest request)
        {
            _clients = clients;
            _request = request;
            _resultDictionary = new Dictionary<string, object>()
            {
                {"values", null},
            };
        }

        public QueryResult GetResult()
        {
            _resultDictionary["values"] = new JArray(
                _clients.
                    Select(c => new JObject(
                            new JProperty("id", c.Id), 
                            new JProperty("createdOn",c.CreatedOn),
                            new JProperty("name",c.Name.Value),
                            new JProperty("description",c.Status),
                            new JProperty("type",c.Type)
                        )
                    )
            );
           
            return new QueryResult(
                new List<Dictionary<string, object>>(){_resultDictionary},
                new AirQualityIndexResponseFormatter()
            );
        }
    }
}