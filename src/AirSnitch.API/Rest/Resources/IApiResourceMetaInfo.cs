using System;
using System.Collections.Generic;

namespace AirSnitch.Api.Rest.Resources
{
    public interface IApiResourceMetaInfo : IEquatable<IApiResourceMetaInfo>
    {
        public ApiResourceName Name { get; }

        public IReadOnlyCollection<ApiResourceColumn> Columns { get; }
    }
}