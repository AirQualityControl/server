using AirSnitch.Core.Domain.Models;

namespace AirSnitch.Infrastructure.Persistence.StorageModels
{
    internal sealed class AirMonitoringStationStorageModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string LocalName { get; set; }

        public bool IsActive { get; set; }

        public string TimeZone { get; set; }

        public City City { get; set; }

        public GeoLocation GeoLocation { get; set; }

        public AirPollutionDataProviderMetaInfoStorageModel DataProviderMetaInfo { get; set; }
    }
}