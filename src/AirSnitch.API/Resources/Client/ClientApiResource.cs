using System.Collections.Generic;

namespace AirSnitch.Api.Resources.Client
{
    public class ClientApiResource : IApiResourceMetaInfo
    {
        public bool Equals(IApiResourceMetaInfo? other)
        {
            throw new System.NotImplementedException();
        }

        public ApiResourceName Name { get; }
        public IReadOnlyCollection<ApiResourceColumn> Columns { get; }
    }
}