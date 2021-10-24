using System.Collections.Generic;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Api.Rest.Resources.DataProvider
{
    public class DataProviderApiResource : IApiResourceMetaInfo
    {
        public ApiResourceName Name
        {
            get => new ApiResourceName("dataProvider");
        }
        public IReadOnlyCollection<ApiResourceColumn> Columns {
            get
            {
                return new List<ApiResourceColumn>()
                {
                    new ApiResourceColumn("id", "id"),
                    new ApiResourceColumn("name", "name"),
                    new ApiResourceColumn("web-site", "web-site")
                };
            }
        }
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
            return Equals((DataProviderApiResource) obj);
        }
        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}