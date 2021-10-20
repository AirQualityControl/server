using System.Collections.Generic;
using System.Linq;
using AirSnitch.Api.Rest.Links;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace AirSnitch.Api.Rest.ResponseBodyFormatters
{
    public class RestfullResponseBodyFormatter : IResponseBodyFormatter
    {
        private readonly HttpRequest _httpRequest;
        private readonly QueryResult _queryResult;
        private readonly IReadOnlyCollection<IApiResourceMetaInfo> _relatedResources;
        private readonly IReadOnlyCollection<IApiResourceMetaInfo> _requestedRelatedResources;

        public RestfullResponseBodyFormatter(
            HttpRequest httpRequest, 
            QueryResult queryResult, 
            IReadOnlyCollection<IApiResourceMetaInfo> relatedResources,
            IReadOnlyCollection<IApiResourceMetaInfo> requestedRelatedResources)
        {
            _httpRequest = httpRequest;
            _queryResult = queryResult;
            _relatedResources = relatedResources;
            _requestedRelatedResources = requestedRelatedResources;
        }

        public string FormatResponse(object responseBody)
        {
            JObject rootObject = new JObject();
            AppendLinks(rootObject);
            if (!_queryResult.IsScalar())
            {
                AppendPageSize(rootObject);
                AppendCurrentPageNumber(rootObject);
                AppendLastPageNumber(rootObject);
            }
            AppendValues(rootObject);
            return rootObject.ToString();
        }
        
        private void AppendLinks(JObject rootObject)
        {
            rootObject["_links"] = new HalLinksContainer(_queryResult, _httpRequest, _relatedResources).Value;
        }

        private void AppendPageSize(JObject rootObject)
        {
            rootObject["pageSize"] = _queryResult.PageOptions.Items;
        }

        private void AppendCurrentPageNumber(JObject rootObject)
        {
            rootObject["currentPageNumber"] = _queryResult.PageOptions.PageNumber;
        }

        private void AppendLastPageNumber(JObject rootObject)
        {
            rootObject["lastPageNumber"] = _queryResult.PageOptions.LastPageNumber;
        }

        private void AppendValues(JObject rootObject)
        {
            rootObject["items"] = GetItems();
        }

        private JArray GetItems()
        {
            var jArray = new JArray();

            var includes = _requestedRelatedResources?.Select(r => r.Name.Value).ToList();
            foreach (var item in _queryResult.GetFormattedValue(includes))
            {
                jArray.Add(
                    new JObject(
                        GetSelfValues(item.ScalarValues),
                        GetIncludeValues(item.IncludedValues)
                    )
                );
            }

            return jArray;
        }
        
        private JProperty GetSelfValues(Dictionary<string, object> selfValues)
        {
            var selfValuesJObject = new JObject();
            foreach (var value in selfValues)
            {
                if (value.Value.GetType() == typeof(JArray))
                {
                    JArray jArrayValue = (JArray) value.Value;
                    selfValuesJObject.Add(new JProperty(value.Key, jArrayValue.First));
                    continue;
                }
                selfValuesJObject.Add(new JProperty(value.Key, value.Value));
            }

            return new JProperty("values", selfValuesJObject);
        }
        
        private JProperty GetIncludeValues(Dictionary<string, object> dataIncludedResources)
        {
            var includesContainer = new JObject();

            foreach (var includedResource  in dataIncludedResources)
            {
                if (includedResource.Value.GetType() != typeof(JArray))
                {
                    includesContainer.Add(
                        new JProperty(includedResource.Key, 
                            new JObject(
                                new JProperty("values", includedResource.Value)
                                )
                            )
                        );
                }
                else
                {
                    var jarray = new JArray();
                    foreach (var item in (JArray) includedResource.Value)
                    {
                        var obj = ((JObject) item);
                        var selft = new JProperty("values", obj);
                        jarray.Add(new JObject(selft));
                    }
                    
                    includesContainer.Add(
                        new JProperty(includedResource.Key,
                               new JObject(
                                   new JProperty("items", 
                                       jarray))
                            )    
                    );
                }
            }

            return new JProperty("includes", includesContainer);
        }
    }
}