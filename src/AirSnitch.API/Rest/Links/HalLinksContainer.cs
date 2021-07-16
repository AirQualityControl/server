using System.Collections.Generic;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Infrastructure.Abstract.Persistence;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace AirSnitch.Api.Rest.Links
{
    public class HalLinksContainer
    {
        private readonly QueryResult _queryResult;
        private readonly HttpRequest _httpRequest;
        private readonly IReadOnlyCollection<IApiResourceMetaInfo> _relatedResourceMetaInfo;

        public HalLinksContainer(QueryResult queryResult, HttpRequest httpRequest, IReadOnlyCollection<IApiResourceMetaInfo> relatedResourceMetaInfo)
        {
            _queryResult = queryResult;
            _httpRequest = httpRequest;
            _relatedResourceMetaInfo = relatedResourceMetaInfo;
        }

        public JObject Value
        {
            get
            {
                var resultValue = new JObject(
                    new SelfLink(_httpRequest).JsonValue
                );

                if (!_queryResult.IsScalar())
                {
                    resultValue.Add(
                        new NextLink(_httpRequest, pageOptions: _queryResult.PageOptions).JsonValue);
                    resultValue.Add(
                        new PrevLink(_httpRequest, _queryResult.PageOptions).JsonValue);
                    resultValue.Add(
                        new LastLink(_httpRequest, _queryResult.PageOptions).JsonValue);
                }
                
                foreach (var relatedResource in _relatedResourceMetaInfo)
                {
                    resultValue.Add(
                        new RelatedResourceLink(
                            _httpRequest, 
                            _queryResult,
                            relatedResource.Name.Value
                        ).JsonValue
                    );
                }
                
                return resultValue;
            }
        }

    }
}