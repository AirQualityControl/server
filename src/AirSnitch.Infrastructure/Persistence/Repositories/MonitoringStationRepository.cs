using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
using AirSnitch.Infrastructure.Persistence.Query;
using AirSnitch.Infrastructure.Persistence.Repositories.Common;
using AirSnitch.Infrastructure.Persistence.StorageModels;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AirSnitch.Infrastructure.Persistence.Repositories
{
    public class MonitoringStationRepository : IMonitoringStationRepository
    {
        private readonly IGenericRepository<MonitoringStationStorageModel> _genericRepository;
        private readonly MongoDbClient _client;
        public MonitoringStationRepository(MongoDbClient client)
        {
            _client = client;
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
            
            var stations =  await _genericRepository.GetByAsync(
                u => u.Id == id
            );

            var monitoringStationStorageModel = stations.SingleOrDefault();
            
            if (monitoringStationStorageModel != default(MonitoringStationStorageModel))
            {
                return monitoringStationStorageModel.MapToDomainModel();
            }

            return MonitoringStation.Empty;
        }

        public async Task<MonitoringStation> FindByProviderNameAsync(string providerStationName)
        {
            var stations =  await _genericRepository.GetByAsync(
                u => u.DisplayName == providerStationName
            );

            var monitoringStationStorageModel = stations.SingleOrDefault();
            
            if (monitoringStationStorageModel != default(MonitoringStationStorageModel))
            {
                return monitoringStationStorageModel.MapToDomainModel();
            }

            return MonitoringStation.Empty;
        }

        public async Task<MonitoringStation> GetNearestStation(GeoCoordinates geoCoordinates, int radius = default, int numberOfStations = 10)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<MonitoringStation>> GetNearestStations(GeoCoordinates geoCoordinates,
            int numberOfStations = 10)
        {
            var monitoringStations = new List<MonitoringStation>(numberOfStations);

            var pipeline = NearestStationsAggregationPipeline.Create(geoCoordinates, numberOfStations);

            var collection = _client.Db.GetCollection<MonitoringStationStorageModel>("airMonitoringStation");

            var cursor = collection.Aggregate<MonitoringStationStorageModel>(pipeline);
            
            Action<MonitoringStationStorageModel> cursorAction = st =>
            {
                monitoringStations.Add(st.MapToDomainModel());
            };

            await cursor.ForEachAsync(cursorAction);

            return monitoringStations;
        }

        public async Task AddAsync(MonitoringStation monitoringStation)
        {
            var storageModel = MonitoringStationStorageModel.CreateFromDomainModel(monitoringStation);
            storageModel.PrimaryKey = ObjectId.GenerateNewId();
            await _genericRepository.SaveAsync(storageModel);
        }

        public async Task UpdateAsync(MonitoringStation monitoringStation)
        {
            var monitoringStationStorageModel = MonitoringStationStorageModel.CreateFromDomainModel(monitoringStation);
            
            await _genericRepository.UpdateByAsync(
                entity: monitoringStationStorageModel, 
                entityMemberSelector:u => u.DisplayName, 
                memberValue: monitoringStation.DisplayName
            );
        }
    }
}