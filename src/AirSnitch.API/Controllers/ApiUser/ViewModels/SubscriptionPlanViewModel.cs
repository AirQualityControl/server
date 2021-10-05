using AirSnitch.Domain.Models;

namespace AirSnitch.Api.Controllers.ApiUser.ViewModels
{
    internal class SubscriptionPlanViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public RequestQuotaViewModel RequestQuota { get; set; }

        public static SubscriptionPlanViewModel BuildFrom(SubscriptionPlan subscriptionPlan)
        {
            return new SubscriptionPlanViewModel()
            {
                Name = subscriptionPlan.Name,
                Description = subscriptionPlan.Description,
                RequestQuota = new RequestQuotaViewModel()
                {
                    Period = subscriptionPlan.RequestQuota.Period.ToString(),
                    MaxNumberOfRequests = subscriptionPlan.RequestQuota.MaxNumberOfRequests
                }
            };
        }
        
        internal class RequestQuotaViewModel
        {
            public string Period;

            public int MaxNumberOfRequests;
        }
    }
}