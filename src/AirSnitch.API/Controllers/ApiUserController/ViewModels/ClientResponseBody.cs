using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.Serialization;
using AirSnitch.Api.Controllers.ApiUser.ViewModels;
using AirSnitch.Api.Rest;
using AirSnitch.Api.Rest.ResponseBodyFormatters;
using AirSnitch.Domain.Models;
using Newtonsoft.Json;

namespace AirSnitch.Api.Controllers.ApiUser
{
    internal class ClientResponseBody : IResponseBody
    {
        private readonly IReadOnlyCollection<ApiClient> _clients;
        
        public ClientResponseBody(IReadOnlyCollection<ApiClient> clients)
        {
            _clients = clients;
        }

        public string Value => Formatter.FormatResponse(ApiClientViewModel.BuildFrom(_clients));
        public bool IsEmpty => false;

        [JsonIgnore]
        protected virtual IResponseBodyFormatter Formatter => new SimpleJsonBodyFormatter();
        
        [JsonIgnore]
        public ContentType ContentType => new ContentType("application/json");
    }
}