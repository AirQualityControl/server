using System.Collections.Generic;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Api.Rest.Resources.Client
{
    public class ClientApiResource : IApiResourceMetaInfo
    {
        private static readonly QueryColumn ResourceQueryColumn = new QueryColumn("id", "clients.id");
        
        private static readonly ApiResourceName ApiUserResourceName = new ApiResourceName("clients");

        private static readonly List<ApiResourceColumn> ResourceColumns = new List<ApiResourceColumn>()
        {
            new ApiResourceColumn("id", "clients.id"),
            new ApiResourceColumn("name", "clients.name"),
            new ApiResourceColumn("description", "clients.description"),
            new ApiResourceColumn("email", "clients.email"),
            new ApiResourceColumn("type", "clients.type"),
            new ApiResourceColumn("createdOn", "clients.createdOn")
        };
        
        public ApiResourceName Name => ApiUserResourceName;
        public IReadOnlyCollection<ApiResourceColumn> Columns => ResourceColumns;
        
        public QueryColumn QueryColumn => ResourceQueryColumn;

        public bool Equals(IApiResourceMetaInfo other)
        {
            return other != null && Name.Equals(other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ClientApiResource) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}