using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Models.Internal
{
    public class UserRepository : IApiUserRepository
    {
        private readonly Dictionary<string, UserDTO> DataProviders = new()
        {
            ["1"] = new UserDTO
            {
                Name = "Vladyslav Kapliuk",
                PhoneNumber = "+380963083105"
            },
            ["2"] = new UserDTO
            {
                Name = "TEst User",
                PhoneNumber = "+380953469584"
            }
        };

        private readonly Dictionary<string, string> RelatedStations = new()
        {
            ["1"] = "2925",
            ["2"] = "1313"
        };

        public Task<string> GetRelatedStationId(string id)
        {
            return Task.FromResult(RelatedStations[id]);
        }

        public Task<UserDTO> GetByIdAsync(string id)
        {
            return Task.FromResult(DataProviders[id]);
        }

        public Task<Page<UserDTO>> GetPage(int pageOffset, int numberOfItems = 50)
        {
            var result = new Page<UserDTO>
            {
                TotalNumberOfItems = DataProviders.Count,
                Items = DataProviders.Skip(pageOffset).Take(numberOfItems).ToDictionary(pair => pair.Key, pair => pair.Value)
            };
            return Task.FromResult(result);
        }
    }
}
