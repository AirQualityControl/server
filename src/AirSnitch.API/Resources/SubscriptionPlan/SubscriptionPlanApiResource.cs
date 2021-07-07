using System.Collections.Generic;

namespace AirSnitch.Api.Resources.SubscriptionPlan
{
    public class SubscriptionPlanApiResource : IApiResourceMetaInfo
    {
        public bool Equals(IApiResourceMetaInfo? other)
        {
            throw new System.NotImplementedException();
        }

        public ApiResourceName Name { get; }
        public IReadOnlyCollection<ApiResourceColumn> Columns { get; }
    }
}