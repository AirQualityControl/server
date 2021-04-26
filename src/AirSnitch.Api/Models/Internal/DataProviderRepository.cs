using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Models.Internal
{
    public class DataProviderRepository : IDataProviderRepository
    {
        private readonly Dictionary<string, DataProviderDTO> DataProviders = new Dictionary<string, DataProviderDTO>
        {
            ["1"] = new DataProviderDTO
            {
                DataUpdateInterval = TimeSpan.Parse("01:00:00"),
                Name = "Save Dnipro",
                WebSiteUri = new Uri("https://www.saveecobot.com/"),
            },
            ["2"] = new DataProviderDTO
            {
                DataUpdateInterval = TimeSpan.Parse("00:30:00"),
                Name = "Misto Lun",
                WebSiteUri = new Uri("https://misto.lun.ua/"),
            }
        };

        public Task<DataProviderDTO> GetByIdAsync(string id)
        {
            return Task.FromResult(DataProviders[id]);
        }

        public Task<Page<DataProviderDTO>> GetPage(int pageOffset, int numberOfItems = 50)
        {
            var result = new Page<DataProviderDTO>
            {
                TotalNumberOfItems = DataProviders.Count,
                Items = DataProviders.Skip(pageOffset).Take(numberOfItems).ToDictionary(pair => pair.Key, pair => pair.Value)
            };
            return Task.FromResult(result);
        }
    }
}
