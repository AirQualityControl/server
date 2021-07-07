using System.Collections.Generic;

namespace AirSnitch.Api.Resources.Client
{
    public class ClientApiResource : IApiResourceMetaInfo
    {
        private static readonly ApiResourceName ApiUserResourceName = new ApiResourceName("clients");

        private static readonly List<ApiResourceColumn> ResourceColumns = new List<ApiResourceColumn>()
        {
            new ApiResourceColumn("name", "name"),
            new ApiResourceColumn("description", "description"),
            new ApiResourceColumn("email", "email"),
            new ApiResourceColumn("type", "type")
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
            return Equals((ClientApiResource) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}