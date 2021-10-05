using System;

namespace AirSnitch.Domain.Models
{
    public class SubscriptionPlan
    {
        private static readonly SubscriptionPlan _basicSubscriptionPlan = new()
        {
            Id = "5ed0055f8bf184fee385bb9e", Name = "Basic", Description = "Basic subscription plan", ExpirationDate = DateTime.MaxValue, RequestQuota = new RequestQuota()
            {
                Period = Period.Day, MaxNumberOfRequests = 100
            }
        };

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime ExpirationDate { get; set; }
        public RequestQuota RequestQuota { get; set; }

        public static SubscriptionPlan Basic => _basicSubscriptionPlan;
    }

    public class RequestQuota
    {
        public Period Period { get; set; }

        public int MaxNumberOfRequests { get; set; }
    }

    public enum Period
    {
        Month,
        Year,
        Day
    }
}