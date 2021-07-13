using System.Collections.Generic;
using AirSnitch.Api.Rest.Links;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Infrastructure.Abstract.Persistence;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace AirSnitch.Api.Controllers
{
    public class RestResponseBody
    {
        //TODO: introduce IResponseBodyFormatter
        private readonly IReadOnlyCollection<IQueryResultEntry> _queryResultEntries;
        private readonly IReadOnlyCollection<IApiResourceMetaInfo> _relatedResources;
        private readonly HttpRequest _request;
        private readonly PageOptions _pageOptions;

        public RestResponseBody(
            IReadOnlyCollection<IQueryResultEntry> queryResultEntries,
            IReadOnlyCollection<IApiResourceMetaInfo> relatedResources,
            HttpRequest request,
            PageOptions pageOptions)
        {
            _request = request;
            _pageOptions = pageOptions;
            _queryResultEntries = queryResultEntries;
            _relatedResources = relatedResources;
        }

        public string Value {
            get
            {
                JObject rootObject = new JObject();
                AppendLinks(rootObject);
                AppendPageSize(rootObject);
                AppendCurrentPageNumber(rootObject);
                AppendLastPageNumber(rootObject);
                AppendValues(rootObject);
                return rootObject.ToString();
            }
        }
        
        private void AppendLinks(JObject rootObject)
        {
            rootObject["_links"] = new HalLinksContainer(_request, _pageOptions).Value;
        }

        private void AppendPageSize(JObject rootObject)
        {
            rootObject["pageSize"] = _pageOptions.Items;
        }

        private void AppendCurrentPageNumber(JObject rootObject)
        {
            rootObject["currentPageNumber"] = _pageOptions.PageNumber;
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
            var jarray = new JArray();

            foreach (var item in _queryResultEntries)
            {
                jarray.Add(
                    new JObject(
                        GetSelfValues(item.ScalarValues),
                        GetIncludeValues(item.IncludedValues)
                    )
                );
            }

            return jarray;
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
        private readonly HttpRequest _httpRequest;
        private readonly PageOptions _pageOptions;

        public HalLinksContainer(HttpRequest httpRequest, PageOptions pageOptions)
        {
            _httpRequest = httpRequest;
            _pageOptions = pageOptions;
        }

        public JObject Value =>
            new JObject(
                new SelfLink(_httpRequest).JsonValue,
                new NextLink(_httpRequest, pageOptions: _pageOptions).JsonValue,
                new PrevLink(_httpRequest, _pageOptions).JsonValue,
                new LastLink(_httpRequest).JsonValue
            );
    }
    public abstract class HalLink
    {
        public abstract string Name { get; }
        
        public abstract string HrefValue { get; }
        
        public JProperty JsonValue =>
            new JProperty(Name,
                new JObject(
                    new JProperty(
                        "href", HrefValue)
                )
            );
    }
}