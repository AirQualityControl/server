using AirSnitch.Api.Models;
using AirSnitch.Api.Models.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.Services
{
    public class ClientService : IClientService
    {
        private Dictionary<string, ApiKey> _apiKeys; 
        public ClientService()
        {
            InitializeUserCollection();
        }

        private void InitializeUserCollection()
        {
            var existingApiKeys = new List<ApiKey>
            {
                new ApiKey(1, "Air Security Corp", "C5BFF7F0-B4DF-475E-A331-F737424F013C", new DateTime(2021, 4, 22),
                    new List<string>
                    {
                        Roles.User,
                    }),
                new ApiKey(2, "Let's Save Planet Corp", "5908D47C-85D3-4024-8C2B-6EC9464398AD", new DateTime(2000, 4, 22),
                    new List<string>
                    {
                        Roles.User
                    }),
                new ApiKey(3, "Admin", "06795D9D-A770-44B9-9B27-03C6ABDB1BAE", new DateTime(2021, 3, 22),
                    new List<string>
                    {
                        Roles.User,
                        Roles.Admin
                    })
            };
            _apiKeys = existingApiKeys.ToDictionary(x => x.Key, x => x);
        }

        public async Task<ApiKey> CreateAsync(UserDTO user)
        {
            var apikey = Guid.NewGuid().ToString();
            var key = new ApiKey(_apiKeys.Count + 1, user.Name, apikey, DateTime.Now, new List<string> { Roles.User });
            _apiKeys.Add(apikey, key);
            return  Task.FromResult(key).Result;
        }

        public async Task<ApiKey> GetStoredApiKey(string providedApiKey)
        {
            _apiKeys.TryGetValue(providedApiKey, out var key);
            return await Task.FromResult(key);
        }

        public Task RevokeKey(string key)
        {
            _apiKeys.Remove(key);
            return Task.CompletedTask;
        }
    }
}
