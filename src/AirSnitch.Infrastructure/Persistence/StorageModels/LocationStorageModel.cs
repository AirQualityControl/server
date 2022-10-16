using System;
using AirSnitch.Domain.Models;
using MongoDB.Bson.Serialization;

namespace AirSnitch.Infrastructure.Persistence.StorageModels
{
    internal class LocationStorageModel
    {
        public string CountryCode { get; set; }
        public string Address { get; set; }
        public double[] GeoCoordinates { get; set; }
        public CityStorageModel City { get; set; }

        public static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<LocationStorageModel>(cm =>
            {
                cm.MapMember(cm => cm.CountryCode).SetElementName("countryCode");
                cm.MapMember(cm => cm.City).SetElementName("city");
                cm.MapMember(cm => cm.GeoCoordinates).SetElementName("geoCoordinates");
                cm.MapMember(cm => cm.Address).SetElementName("address");
            });
        }

        public Location MapToDomainModel()
        {
            var location = new Location();
            location.SetAddress(Address);
            location.SetCountry(Country.UA);
            location.SetCity(City?.MapToDomainModel());
            location.SetGeoCoordinates(new GeoCoordinates(){Longitude = GeoCoordinates[0], Latitude = GeoCoordinates[1]});
            return location;
        }

        public static LocationStorageModel MapFromDomainModel(Location location)
        {
            var geoCoordinates = location.GeoCoordinates();
            return new LocationStorageModel()
            {
                Address = location.GetAddress(),
                CountryCode = location.GetCountry().Code,
                GeoCoordinates = new []{ geoCoordinates.Longitude, geoCoordinates.Latitude },
                City = new CityStorageModel()
                {
                    Code = location.GetCity().Code,
                    Name = location.GetCity().Name,
                }
            };
        }
    }
    internal class CityStorageModel
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public City MapToDomainModel()
        {
            return new City(name: Name, code: Code);
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