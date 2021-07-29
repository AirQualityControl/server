using System.Collections.Generic;
using AirSnitch.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AirSnitch.Api.Controllers
{
    public class ClientResponseBody : IResponseBody
    {
        private readonly IReadOnlyCollection<ApiClient> _clients;

        public ClientResponseBody(IReadOnlyCollection<ApiClient> clients)
        {
            _clients = clients;
        }

        //TODO: use formatter instead
        public string Value => JsonConvert.SerializeObject(
            ApiClientViewModel.BuildFrom(_clients),new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }
        );
    }
}