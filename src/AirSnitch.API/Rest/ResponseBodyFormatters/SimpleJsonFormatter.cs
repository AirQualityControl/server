using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AirSnitch.Api.Rest.ResponseBodyFormatters
{
    public class SimpleJsonBodyFormatter : IResponseBodyFormatter
    {
        public string FormatResponse(object responseBody)
        {
            var body = responseBody as IEnumerable;
            return body != null ? BuildJArrayFromEnumeration(body).ToString() : BuildJObjectFromObject(responseBody).ToString();
        }
        private JObject BuildJArrayFromEnumeration(IEnumerable responseBodyItems)
        {
            var rootObject = new JObject();

            var jArray = new JArray();

            foreach (var item in responseBodyItems)
            {
                jArray.Add(BuildJObjectFromObject(item));
            }

            rootObject["items"] = jArray;
            return rootObject;
        }
        private JObject BuildJObjectFromObject(object targetObject)
        {
            return new JObject(
                new JProperty("values", 
                    JObject.Parse(
                        JsonConvert.SerializeObject(targetObject)))
                );
        }
    }
}