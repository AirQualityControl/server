using System.Collections.Generic;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Api.Rest.Resources.MonitoringStation
{
    public class MonitoringStationResource : IApiResourceMetaInfo
    {
        public ApiResourceName Name => new ApiResourceName("airMonitoringStation");
        public IReadOnlyCollection<ApiResourceColumn> Columns =>
            new List<ApiResourceColumn>()
            {
                new ApiResourceColumn("id", "id"),
                new ApiResourceColumn("displayName", "displayName"),
                new ApiResourceColumn("location", "location"),
            };

        public QueryColumn QueryColumn => new PrimaryColumn();

        public bool Equals(IApiResourceMetaInfo other)
        {
            return other != null && Name.Equals(other.Name);
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MonitoringStationResource) obj);
        }
        
        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}