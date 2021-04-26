using AirSnitch.Api.Models;
using AirSnitch.Api.Models.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.Services
{
    public class DataProviderService : IDataProviderService
    {
        IDataProviderRepository _dataproviderRepository;
        public DataProviderService(IDataProviderRepository dataproviderRepository)
        {
            _dataproviderRepository = dataproviderRepository;
        }
        public async Task<DataProviderDTO> GetByIdAsync(string stationId)
        {
            var station = await _dataproviderRepository.GetByIdAsync(stationId);
            return station;
        }

        public async Task<(Dictionary<string, DataProviderDTO>, int)> GetPaginated(int limit, int offset)
        {
            var page = await _dataproviderRepository.GetPage(pageOffset: offset, numberOfItems: limit);
            return (page.Items, page.TotalNumberOfItems);
        }
    }
}
