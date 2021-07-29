using System.Collections.Generic;

namespace AirSnitch.Domain.Models
{
    public class SubscriptionPlan
    {
        private static readonly SubscriptionPlan _basicSubscriptionPlan = new()
        {
            Name = "Basic", Description = "Basic subscription plan", RequestQuota = new RequestQuota()
            {
                Period = Period.Day, MaxNumberOfRequests = 100
            }
        };
        
        
        public string Name { get; private set; }

        public string Description { get; private set; }

        public RequestQuota RequestQuota { get; private set; }

        public static SubscriptionPlan Basic => _basicSubscriptionPlan;
    }

    public class RequestQuota
    {
        public Period Period { get; set; }

        public ulong MaxNumberOfRequests { get; set; }
    }

    public enum Period
    {
        Month,
        Year,
        Day
    }
}