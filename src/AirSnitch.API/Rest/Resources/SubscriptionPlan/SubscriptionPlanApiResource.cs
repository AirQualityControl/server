using System.Collections.Generic;

namespace AirSnitch.Api.Rest.Resources.SubscriptionPlan
{
    public class SubscriptionPlanApiResource : IApiResourceMetaInfo
    {
        private static readonly ApiResourceName ApiUserResourceName = new ApiResourceName("subscriptionPlan");

        private static readonly List<ApiResourceColumn> ResourceColumns = new List<ApiResourceColumn>()
        {
            new ApiResourceColumn("id", "name"),
            new ApiResourceColumn("name", "description"),
            new ApiResourceColumn("description", "description"),
            new ApiResourceColumn("parameters", "parameters")
        };
        
        public ApiResourceName Name => ApiUserResourceName;

        public IReadOnlyCollection<ApiResourceColumn> Columns => ResourceColumns;

        public bool Equals(IApiResourceMetaInfo other)
        {
            return other != null && Name.Equals(other.Name);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SubscriptionPlanApiResource) obj);
        }
        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}