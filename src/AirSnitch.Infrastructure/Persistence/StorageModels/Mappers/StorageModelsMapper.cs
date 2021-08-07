namespace AirSnitch.Infrastructure.Persistence.StorageModels.Mappers
{
    internal static class StorageModelsMapper
    {
        public static void RegisterMapping()
        {
            ApiUserStorageModel.RegisterDbMap();
        }
    }
}