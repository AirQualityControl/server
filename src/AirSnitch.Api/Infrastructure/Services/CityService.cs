using AirSnitch.Api.Models;
using AirSnitch.Api.Models.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.Services
{
    public class CityService : ICityService
    {
        readonly ICityRepository _cityRepository;
        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public async Task<CityDTO> GetByIdAsync(string stationId)
        {
            var station = await _cityRepository.GetByIdAsync(stationId);
            return station;
        }

        public async Task<(Dictionary<string, CityDTO>, int)> GetPaginated(int limit, int offset)
        {
            var page = await _cityRepository.GetPage(pageOffset: offset, numberOfItems: limit);
            return (page.Items, page.TotalNumberOfItems);
        }
    }
}
