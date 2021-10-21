using AirSnitch.Domain.Models;
using MongoDB.Bson.Serialization;

namespace AirSnitch.Infrastructure.Persistence.StorageModels
{
    internal class LocationStorageModel
    {
        public string CountryCode { get; set; }
        public CityStorageModel City { get; set; }
        public string Address { get; set; }

        public static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<LocationStorageModel>(cm =>
            {
                cm.MapMember(cm => cm.CountryCode).SetElementName("countryCode");
                cm.MapMember(cm => cm.Address).SetElementName("address");
            });
        }

        public Location MapToDomainModel()
        {
            var location = new Location();
            location.SetAddress(Address);
            location.SetCountry(Country.UA);
            location.SetCity(City?.MapToDomainModel());

            return location;
        }
    }

    internal class CityStorageModel
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public City MapToDomainModel()
        {
            return City.FromString(Code);
        }

        public static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<CityStorageModel>(cm =>
            {
                cm.MapMember(cm => cm.Code).SetElementName("code");
                cm.MapMember(cm => cm.Name).SetElementName("name");
            });
        }
    }
}