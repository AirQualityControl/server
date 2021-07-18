using System;
using System.Collections.Generic;
using System.Linq;

namespace AirSnitch.Api.Rest.Resources.Registry
{
    public class ApiResourceRegistry : IApiResourceRegistry
    {
        private readonly List<IApiResourceMetaInfo> _apiResourceMetaInfo = new List<IApiResourceMetaInfo>();
        private readonly object _locker = new object();
        public void RegisterApiResource(IApiResourceMetaInfo apiResourceMetaInfo)
        {
            lock (_locker)
            {
                _apiResourceMetaInfo.Add(apiResourceMetaInfo);
            }
        }

        public IReadOnlyCollection<IApiResourceMetaInfo> ApiResources => _apiResourceMetaInfo;

        public IReadOnlyCollection<IApiResourceMetaInfo> GetBy(Func<IApiResourceMetaInfo, bool> predicate)
        {
            return ApiResources
                .Where(predicate)
                .ToList();
        }
    }
}