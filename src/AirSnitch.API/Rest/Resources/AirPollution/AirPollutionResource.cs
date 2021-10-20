using System.Collections.Generic;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Api.Rest.Resources.AirPollution
{
    public class AirPollutionResource : IApiResourceMetaInfo
    {
        public ApiResourceName Name => new ApiResourceName("airPollution");

        public IReadOnlyCollection<ApiResourceColumn> Columns =>
            new List<ApiResourceColumn>()
            {
                new ApiResourceColumn("particles", "particles")
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
            return Equals((AirPollutionResource) obj);
        }
        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}