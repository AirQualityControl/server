using AirSnitch.Domain.Models;

namespace AirSnitch.Infrastructure.Persistence.StorageModels.Mappers
{
    internal static class StorageModelsMapper
    {
        public static void RegisterMapping()
        {
            ApiUserStorageModel.RegisterDbMap();
            ClientStorageModel.RegisterDbMap();
            SubscriptionPlanStorageModel.RegisterDbMap();
            SubscriptionPlanQuotaStorageModel.RegisterDbMap();
            ApiKeyStorageModel.RegisterDbMap();

            MonitoringStationStorageModel.RegisterDbMap();
            LocationStorageModel.RegisterDbMap();
            CityStorageModel.RegisterDbMap();
            AirQualityIndexStorageModel.RegisterDbMap();
            AirPollutionStorageModel.RegisterDbMap();
            DataProviderStorageModel.RegisterDbMap();
            ParticleStorageModel.RegisterDbMap();
        }
    }
}