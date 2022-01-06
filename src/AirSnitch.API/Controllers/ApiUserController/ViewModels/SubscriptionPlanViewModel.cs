using System.Collections.Generic;
using AirSnitch.Api.Controllers.AirQualityIndexController.ViewModel;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using Newtonsoft.Json.Linq;

namespace AirSnitch.Api.Controllers.ApiUserController.ViewModels
{
    internal class SubscriptionPlanViewModel
    {
        private readonly SubscriptionPlan _subscriptionPlan;
        public SubscriptionPlanViewModel(SubscriptionPlan subscriptionPlan)
        {
            _subscriptionPlan = subscriptionPlan;
        }
        
        public QueryResult GetResult()
        {
            var resultDictionary = new Dictionary<string, object>()
            {
                { "name", _subscriptionPlan.Id },
                { "description", _subscriptionPlan.Name },
            };

            return new QueryResult(
                new List<Dictionary<string, object>>(){resultDictionary},
                new AirQualityIndexResponseFormatter()
            );
        }
    }
}