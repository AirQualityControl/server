using AirSnitch.Api.Models.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.Auth
{
    public class InMemoryGetApiKeyQuery : IGetApiKeyQuery
    {
        readonly IClientService _dummyClientService;

        public InMemoryGetApiKeyQuery(IClientService dummyUserService)
        {
            _dummyClientService = dummyUserService;
            
        }

        public async Task<ApiKey> Execute(string providedApiKey)
        {
            return await _dummyClientService.GetStoredApiKey(providedApiKey);
        }
    }
}
