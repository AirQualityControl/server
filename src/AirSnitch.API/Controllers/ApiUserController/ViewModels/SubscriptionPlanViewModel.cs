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
        private Dictionary<string, object> _resultDictionary;

        public SubscriptionPlanViewModel(SubscriptionPlan subscriptionPlan)
        {
            _subscriptionPlan = subscriptionPlan;
            _resultDictionary = new Dictionary<string, object>()
            {
                {"values", null},
            };
        }
        
        public QueryResult GetResult()
        {
            _resultDictionary["values"] = new JObject(
                new JProperty("name", _subscriptionPlan.Name),
                new JProperty("description", _subscriptionPlan.Description)
            );

            return new QueryResult(
                new List<Dictionary<string, object>>(){_resultDictionary},
                new AirQualityIndexResponseFormatter()
            );
        }
    }
}