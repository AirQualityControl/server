using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
using AirSnitch.Infrastructure.Persistence.Query;
using AirSnitch.Infrastructure.Persistence.StorageModels;

namespace AirSnitch.Infrastructure.Persistence.Repositories
{
    public class ApiUserRepository : IApiUserRepository
    {
        private readonly IGenericRepository<ApiUserStorageModel> _genericRepository;

        public ApiUserRepository(MongoDbClient client)
        {
            _genericRepository = new MongoDbGenericRepository<ApiUserStorageModel>(client, "apiUser");
        }
        
        public async Task<ApiUser> FindById(string id)
        {
            return await Task.FromResult(
                new ApiUser()
                {
                    Profile = new ApiUserProfile()
                    {
                        Name = new UserName("Artur"),
                        LastName = new LastName("Lavrov"),
                        Email = new Email("arturstylus@gmail.com"),
                    },
                    SubscriptionPlan = SubscriptionPlan.Basic,
                    Clients = new List<ApiClient>()
                    {
                        new ApiClient()
                        {
                            CreatedOn = DateTime.Now,
                            Description = new ClientDescription("My awesome IoT App"),
                            Name = new ClientName("IoT App"),
                            Status = ClientStatus.Active,
                            Type = ClientType.Production
                        }
                    }
                });
        }

        public async Task<QueryResult> ExecuteQueryFromSchemeAsync(QueryScheme queryScheme)
        {
            var query = MongoDbQuery.CreateFromScheme(queryScheme);

            var queryResultTask = _genericRepository.ExecuteQueryAsync(query);
            var totalNumberOfDocumentsTask = _genericRepository.Count;
            
            await Task.WhenAll(queryResultTask, totalNumberOfDocumentsTask);

            return new QueryResult(queryResultTask.Result.Value, 
                new PageOptions(
                    pageNumber: queryScheme.PageOptions.PageNumber, 
                    totalNumberOfItems: totalNumberOfDocumentsTask.Result, 
                    itemsPerPage:queryScheme.PageOptions.ItemsLimit));
        }
    }
}