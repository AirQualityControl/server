using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AirSnitch.Domain.Models;
using MongoDB.Bson.Serialization;

namespace AirSnitch.Infrastructure.Persistence.StorageModels
{
    internal class DataProviderStorageModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string WebSite { get; set; }

        public static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<DataProviderStorageModel>(cm =>
            {
                cm.MapMember(cm => cm.Id).SetElementName("id");
                cm.MapMember(cm => cm.Name).SetElementName("name");
                cm.MapMember(cm => cm.WebSite).SetElementName("web-site");
            });
        }

        public MonitoringStationOwner MapToDomainModel()
        {
            var owner = new MonitoringStationOwner(Id, Name);
            owner.SetWebSite(new Uri(WebSite));
            return owner;
        }

        public static DataProviderStorageModel MapFromDomainModel(MonitoringStationOwner stationOwner)
        {
            return new DataProviderStorageModel()
            {
                Id = stationOwner.Id,
                Name = stationOwner.Name,
                WebSite = stationOwner.WebSite.ToString()
            };
        }
    }
}