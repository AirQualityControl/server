using System.Collections.Generic;
using AirSnitch.Infrastructure.Abstract.Persistence;
using Newtonsoft.Json.Linq;

namespace AirSnitch.Api.Controllers
{
    public class RestResponseBody
    {
        //TODO: introduce IResponseBodyMediaFormatter
        
        private readonly IReadOnlyCollection<IQueryResultEntry> _queryResultEntries;
        public RestResponseBody(IReadOnlyCollection<IQueryResultEntry> queryResultEntries)
        {
            _queryResultEntries = queryResultEntries;
        }
        
        public string GetBody()
        {
            JObject rootObject = new JObject();
            AppendLinks(rootObject);
            AppendPageSize(rootObject);
            AppendCurrentPageNumber(rootObject);
            AppendLastPageNumber(rootObject);
            AppendValues(rootObject);
            return rootObject.ToString();
        }

        private void AppendLinks(JObject rootObject)
        {
            rootObject["_links"] = new HalLinksContainer().Value;
        }

        private void AppendPageSize(JObject rootObject)
        {
            rootObject["pageSize"] = 50;
        }

        private void AppendCurrentPageNumber(JObject rootObject)
        {
            rootObject["currentPageNumber"] = 1;
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
        public JObject Value {
            get
            {
                return new JObject(
                    new JProperty("self",
                        new JObject(
                            new JProperty(
                                "href", "www.api.airsnitch.com/v1/apiuser")
                        )
                    ),
                    new JProperty("next",
                        new JObject(
                            new JProperty(
                                "href", "www.api.airsnitch.com/v1/apiuser")
                        )
                    ),
                    new JProperty("prev",
                        new JObject(
                            new JProperty(
                                "href", "www.api.airsnitch.com/v1/apiuser")
                        )
                    ),
                    new JProperty("last",
                        new JObject(
                            new JProperty(
                                "href", "www.api.airsnitch.com/v1/apiuser")
                        )
                    )
                );
            }
        }
    }
    
    public interface IHalLink
    {
        public string Name { get; }

        public string HrefValue { get; }

        public JProperty JsonValue { get; }
    }

    public class SelfLink : IHalLink
    {
        public string Name => "www.api.airsnitch.com/v1/apiuser";

        public string HrefValue { get; }

        public JProperty JsonValue =>
            new JProperty("self",
                new JObject(
                    new JProperty(
                        "href", "www.api.airsnitch.com/v1/apiuser")
                )
            );
    }

    public class NextLink : IHalLink
    {
        public string Name { get; }
        
        public string HrefValue { get; }
        
        public JProperty JsonValue =>
            new JProperty("next",
                new JObject(
                    new JProperty(
                        "href", "www.api.airsnitch.com/v1/apiuser")
                )
            );
    }

    public class LastLink : IHalLink
    {
        public string Name { get; }
        
        public string HrefValue { get; }
        
        public JProperty JsonValue =>
            new JProperty("last",
                new JObject(
                    new JProperty(
                        "href", "www.api.airsnitch.com/v1/apiuser")
                )
            );
    }
}