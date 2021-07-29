using System.Net.Mime;
using AirSnitch.Api.Controllers.ApiUser.ViewModels;
using AirSnitch.Api.Rest;
using AirSnitch.Api.Rest.ResponseBodyFormatters;
using AirSnitch.Domain.Models;
using Newtonsoft.Json;

namespace AirSnitch.Api.Controllers.ApiUser
{
    internal class SubscriptionPlanResponseBody : IResponseBody
    {
        private readonly SubscriptionPlan _subscriptionPlan;

        public SubscriptionPlanResponseBody(SubscriptionPlan subscriptionPlan)
        {
            _subscriptionPlan = subscriptionPlan;
        }

        public string Value => Formatter.FormatResponse(SubscriptionPlanViewModel.BuildFrom(_subscriptionPlan));

        [JsonIgnore]
        protected virtual IResponseBodyFormatter Formatter => new SimpleJsonBodyFormatter();
        
        [JsonIgnore]
        public ContentType ContentType => new ContentType("application/json");
    }
}