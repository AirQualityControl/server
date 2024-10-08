using System.Collections.Generic;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Api.Rest.Resources.ApiUser
{
    internal class ApiUserResource : IApiResourceMetaInfo
    {
        private static readonly QueryColumn _queryColumn = new PrimaryColumn();
        
        private static readonly ApiResourceName ApiUserResourceName = new ApiResourceName("apiUser");

        private static readonly List<ApiResourceColumn> ResourceColumns = new List<ApiResourceColumn>()
        {
            new ApiResourceColumn("id", "id"),
            new ApiResourceColumn("firstName", "firstName"),
            new ApiResourceColumn("lastName", "lastName"),
            new ApiResourceColumn("email", "email"),
            new ApiResourceColumn("profilePicUrl", "profilePicUrl")
        };
        public ApiResourceName Name => ApiUserResourceName;
        public IReadOnlyCollection<ApiResourceColumn> Columns => ResourceColumns;
        
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
            return Equals((ApiUserResource) obj);
        }
        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}