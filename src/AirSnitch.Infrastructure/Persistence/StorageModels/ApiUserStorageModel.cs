using AirSnitch.Infrastructure.Abstract.Persistence;

namespace AirSnitch.Infrastructure.Persistence.StorageModels
{
    public class ApiUserStorageModel
    {
        public static void RegisterDbMap()
        {
            return;
        }

        public string CollectionName => "apiUser";
    }
}