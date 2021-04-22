using AirSnitch.Api.Models.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.Auth
{
    public class InMemoryGetApiKeyQuery : IGetApiKeyQuery
    {
        IDummyUserService _dummyUserService;

        public InMemoryGetApiKeyQuery(IDummyUserService dummyUserService)
        {
            _dummyUserService = dummyUserService;
            
        }

        public async Task<ApiKey> Execute(string providedApiKey)
        {
            return await _dummyUserService.GetStoredApiKey(providedApiKey);
        }
    }
}
