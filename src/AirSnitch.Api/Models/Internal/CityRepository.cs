using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirSnitch.Api.Models.Internal
{
    public class CityRepository : ICityRepository
    {
        private readonly Dictionary<string, CityDTO> Cities =  new Dictionary<string, CityDTO>
            {
                ["1"] = new CityDTO
                {
                    Code = "Vinnytsya",
                    CountryCode = "UA",
                    FriendlyName = "Vinnytsya",
                },
                ["2"] = new CityDTO
                {
                    Code = "Zhytomyr",
                    CountryCode = "UA",
                    FriendlyName = "Zhytomyr"
                },
                ["3"] = new CityDTO
                {
                    Code = "Rivne",
                    CountryCode = "UA",
                    FriendlyName = "Rivne"
                },
                ["4"] = new CityDTO
                {
                    Code = "Lutsk",
                    CountryCode = "UA",
                    FriendlyName = "Lutsk"
                },
                ["5"] = new CityDTO
                {
                    Code = "Chernihiv",
                    CountryCode = "UA",
                    FriendlyName = "Chernihiv"
                },
                ["6"] = new CityDTO
                {
                    Code = "Sumy",
                    CountryCode = "UA",
                    FriendlyName = "Sumy"
                },
                ["7"] = new CityDTO
                {
                    Code = "Kharkiv",
                    CountryCode = "UA",
                    FriendlyName = "Kharkiv"
                },
                ["8"] = new CityDTO
                {
                    Code = "Luhansk",
                    CountryCode = "UA",
                    FriendlyName = "Luhansk"
                },
                ["9"] = new CityDTO
                {
                    Code = "Lviv",
                    CountryCode = "UA",
                    FriendlyName = "Lviv"
                },
                ["10"] = new CityDTO
                {
                    Code = "Ternopil",
                    CountryCode = "UA",
                    FriendlyName = "Ternopil"
                },
                ["11"] = new CityDTO
                {
                    Code = "Khmelnytskyi",
                    CountryCode = "UA",
                    FriendlyName = "Khmelnytskyi"
                },
                ["12"] = new CityDTO
                {
                    Code = "Cherkasy",
                    CountryCode = "UA",
                    FriendlyName = "Cherkasy"
                },
                ["13"] = new CityDTO
                {
                    Code = "Poltava",
                    CountryCode = "UA",
                    FriendlyName = "Poltava"
                },
                ["14"] = new CityDTO
                {
                    Code = "Dnipro",
                    CountryCode = "UA",
                    FriendlyName = "Dnipro"
                },
                ["15"] = new CityDTO
                {
                    Code = "Donetsk",
                    CountryCode = "UA",
                    FriendlyName = "Donetsk"
                },
                ["16"] = new CityDTO
                {
                    Code = "Uzhhorod",
                    CountryCode = "UA",
                    FriendlyName = "Uzhhorod"
                },
                ["17"] = new CityDTO
                {
                    Code = "Ivano-Frankivsk",
                    CountryCode = "UA",
                    FriendlyName = "Ivano-Frankivsk"
                },
                ["18"] = new CityDTO
                {
                    Code = "Chernivtsi",
                    CountryCode = "UA",
                    FriendlyName = "Chernivtsi"
                },
                ["19"] = new CityDTO
                {
                    Code = "Kropyvnytskyi",
                    CountryCode = "UA",
                    FriendlyName = "Kropyvnytskyi"
                },
                ["20"] = new CityDTO
                {
                    Code = "Zaporizhzhya",
                    CountryCode = "UA",
                    FriendlyName = "Zaporizhzhya"
                },
                ["21"] = new CityDTO
                {
                    Code = "Odesa",
                    CountryCode = "UA",
                    FriendlyName = "Odesa"
                },
                ["22"] = new CityDTO
                {
                    Code = "Mykolayiv",
                    CountryCode = "UA",
                    FriendlyName = "Mykolayiv"
                },
                ["23"] = new CityDTO
                {
                    Code = "Kherson",
                    CountryCode = "UA",
                    FriendlyName = "Kherson"
                },
                ["24"] = new CityDTO
                {
                    Code = "Simpferopol",
                    CountryCode = "UA",
                    FriendlyName = "Simpferopol"
                },
                ["25"] = new CityDTO
                {
                    Code = "Sevastopol",
                    CountryCode = "UA",
                    FriendlyName = "Sevastopol"
                }
            };

        public Task<CityDTO> GetByIdAsync(string id)
        {
            return Task.FromResult(Cities[id]);
        }

        public Task<Page<CityDTO>> GetPage(int pageOffset, int numberOfItems = 50)
        {
            var result = new Page<CityDTO>
            {
                TotalNumberOfItems = Cities.Count,
                Items = Cities.Skip(pageOffset).Take(numberOfItems).ToDictionary(pair => pair.Key, pair => pair.Value)
            };
            return Task.FromResult(result);
        }
    }
}