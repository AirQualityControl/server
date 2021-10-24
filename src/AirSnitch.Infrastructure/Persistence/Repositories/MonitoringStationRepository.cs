using System;
using System.Linq;
using System.Threading.Tasks;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
using AirSnitch.Infrastructure.Persistence.Query;
using AirSnitch.Infrastructure.Persistence.Repositories.Common;
using AirSnitch.Infrastructure.Persistence.StorageModels;

namespace AirSnitch.Infrastructure.Persistence.Repositories
{
    public class MonitoringStationRepository : IMonitoringStationRepository
    {
        private readonly IGenericRepository<MonitoringStationStorageModel> _genericRepository;

        public MonitoringStationRepository(MongoDbClient client)
        {
            _genericRepository = new MongoDbGenericRepository<MonitoringStationStorageModel>(client, "airMonitoringStation");
        }
        
        public async Task<QueryResult> ExecuteQueryFromSchemeAsync(QueryScheme queryScheme)
        {
            var query = MongoDbQuery.CreateFromScheme(queryScheme);

            var queryResultTask = _genericRepository.ExecuteQueryAsync(query);
            var totalNumberOfDocumentsTask = _genericRepository.Count;
            
            await Task.WhenAll(queryResultTask, totalNumberOfDocumentsTask);

            return new QueryResult(
                queryResultTask.Result,
                new MongoDbQueryResultFormatter(),
                new PageOptions(
                    pageNumber: queryScheme.PageOptions.PageNumber, 
                    totalNumberOfItems: totalNumberOfDocumentsTask.Result, 
                    itemsPerPage:queryScheme.PageOptions.ItemsLimit)
            );
        }

        public async Task<MonitoringStation> GetByIdAsync(string id)
        {
            Guid.Parse(id);
            
            var users =  await _genericRepository.GetByAsync(
                u => u.Id == id
            );

            var monitoringStationStorageModel = users.SingleOrDefault();
            
            if (monitoringStationStorageModel != default(MonitoringStationStorageModel))
            {
                return monitoringStationStorageModel.MapToDomainModel();
            }
            throw new ItemNotFoundException();
        }

        public async Task<MonitoringStation> FindByIdAsync(string id)
        {
            Guid.Parse(id);
            
            var users =  await _genericRepository.GetByAsync(
                u => u.Id == id
            );

            var monitoringStationStorageModel = users.SingleOrDefault();
            
            if (monitoringStationStorageModel != default(MonitoringStationStorageModel))
            {
                return monitoringStationStorageModel.MapToDomainModel();
            }

            return MonitoringStation.Empty;
        }

        public Task<MonitoringStation> GetNearestStation(GeoCoordinates geoCoordinates, int radius = default)
        {
            var monitoringStation = new MonitoringStation();
            return Task.FromResult(monitoringStation);
        }
    }
}