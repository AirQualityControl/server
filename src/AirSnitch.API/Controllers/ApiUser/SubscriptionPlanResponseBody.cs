using AirSnitch.Api.Controllers.ApiUser.ViewModels;
using AirSnitch.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AirSnitch.Api.Controllers
{
    public class SubscriptionPlanResponseBody : IResponseBody
    {
        private readonly SubscriptionPlan _subscriptionPlan;


        public SubscriptionPlanResponseBody(SubscriptionPlan subscriptionPlan)
        {
            _subscriptionPlan = subscriptionPlan;
        }
        
        public string Value
        {
            get => JsonConvert.SerializeObject(
                SubscriptionPlanViewModel.BuildFrom(_subscriptionPlan),
                new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            );
        }
    }
}