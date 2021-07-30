using System;
using System.Collections.Generic;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Api.Rest.Resources
{
    public interface IApiResourceMetaInfo : IEquatable<IApiResourceMetaInfo>
    {
        public ApiResourceName Name { get; }

        public IReadOnlyCollection<ApiResourceColumn> Columns { get; }

        public QueryColumn QueryColumn { get; }
    }
}