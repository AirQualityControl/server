using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirSnitch.Core.Domain.Exceptions;
using AirSnitch.Core.Domain.Models;
using AirSnitch.Core.Infrastructure.Persistence;
using AirSnitch.Infrastructure.Persistence.Mongo.Mappers;
using AirSnitch.Infrastructure.Persistence.StorageModels;
using DeclarativeContracts.Functions;
using DeclarativeContracts.Precondition;
using MongoDB.Driver;

namespace AirSnitch.Infrastructure.Persistence.Repositories
{
    /// <inheritdoc cref="IAirMonitoringStationRepository" />
    internal class AirMonitoringStationRepository : BaseRepository<AirMonitoringStationStorageModel>, IAirMonitoringStationRepository
    {
        private readonly IAirMonitoringStationModelMapper _mapper;

        public AirMonitoringStationRepository(IAirMonitoringStationModelMapper mapper)
        {
            _mapper = mapper;
        }
        
        protected override string CollectionName => "airMonitoringStation";

        ///<inheritdoc/>
        public async Task<AirMonitoringStation> GetByIdAsync(string stationId)
        {
            Require.That(stationId, Is.NotNullOrEmptyString);
            
            var stations = await GetByAsync(
                predicate: st => st.Id == stationId);
            
            var station = stations.SingleOrDefault();
            
            if (station != default(AirMonitoringStationStorageModel))
            {
                return _mapper.MapToDomainModel(station);
            }

            throw new AirMonitoringStationNotFoundException();
        }

        ///<inheritdoc/>
        public async Task<IReadOnlyCollection<AirMonitoringStation>> GetTopNearestStationsAsync(GeoLocation geoLocation,
            int numberOfNearestStations)
        {
            Require.That(numberOfNearestStations, Is.Positive);

            LinkedList<AirMonitoringStation> nearestStations = new LinkedList<AirMonitoringStation>();
            
            var pipeline = NearestStationsAggregationPipeline.Create(geoLocation, numberOfNearestStations);
            
            var cursor = Collection.Aggregate<AirMonitoringStationStorageModel>(pipeline);

            Action<AirMonitoringStationStorageModel> cursorAction = st =>
            {
                nearestStations.AddLast(_mapper.MapToDomainModel(st));
            };
            
            await cursor.ForEachAsync(cursorAction);

            return nearestStations;
        }
    }
}
