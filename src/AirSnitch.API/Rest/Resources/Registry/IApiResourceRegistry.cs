using System;
using System.Collections.Generic;

namespace AirSnitch.Api.Rest.Resources.Registry
{
    public interface IApiResourceRegistry
    {
        void RegisterApiResource(IApiResourceMetaInfo apiResourceMetaInfo);

        IReadOnlyCollection<IApiResourceMetaInfo> ApiResources { get; }

        IReadOnlyCollection<IApiResourceMetaInfo> GetBy(Func<IApiResourceMetaInfo, bool> predicate);
    }
}