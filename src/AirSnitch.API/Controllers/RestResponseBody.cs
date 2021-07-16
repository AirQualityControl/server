using System.Collections.Generic;
using AirSnitch.Api.Rest.Links;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Api.Rest.Resources.Client;
using AirSnitch.Api.Rest.Resources.SubscriptionPlan;
using AirSnitch.Infrastructure.Abstract.Persistence;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace AirSnitch.Api.Controllers
{
    public class RestResponseBody
    {
        private readonly HttpRequest _httpRequest;
        private readonly QueryResult _queryResult;
        //TODO: introduce IResponseBodyFormatter
        private readonly IReadOnlyCollection<IApiResourceMetaInfo> _relatedResources;

        public RestResponseBody(HttpRequest httpRequest, 
            QueryResult queryResult, 
            IReadOnlyCollection<IApiResourceMetaInfo> relatedResources)
        {
            _httpRequest = httpRequest;
            _queryResult = queryResult;
            _relatedResources = relatedResources;
        }

        public string Value {
            get
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
            rootObject["lastPageNumber"] = 52;
        }

        private void AppendValues(JObject rootObject)
        {
            rootObject["items"] = GetItems();
        }

        private JArray GetItems()
        {
            var jArray = new JArray();

            foreach (var item in _queryResult.Value)
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
                    resultValue.Add(new PrevLink(_httpRequest, _queryResult.PageOptions).JsonValue);
                    resultValue.Add(new LastLink(_httpRequest).JsonValue);
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