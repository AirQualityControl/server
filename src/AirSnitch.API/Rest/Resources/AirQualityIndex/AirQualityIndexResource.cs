using System.Collections.Generic;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Api.Rest.Resources.AirQualityIndex
{
    public class AirQualityIndexResource : IApiResourceMetaInfo
    {
        public ApiResourceName Name
        {
            get => new ApiResourceName("airQualityIndex");
        }
        public IReadOnlyCollection<ApiResourceColumn> Columns { get; }
        public QueryColumn QueryColumn { get; }
        
        public bool Equals(IApiResourceMetaInfo other)
        {
            return other != null && Name.Equals(other.Name);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AirQualityIndexResource) obj);
        }
        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}