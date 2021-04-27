using AirSnitch.Api.Models;
using AirSnitch.Api.Models.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.Services
{
    public interface IClientService
    {
        Task<ApiKey> CreateAsync(UserDTO user);
        Task<ApiKey> GetStoredApiKey(string providedApiKey);
        Task RevokeKey(string key);
    }
}
