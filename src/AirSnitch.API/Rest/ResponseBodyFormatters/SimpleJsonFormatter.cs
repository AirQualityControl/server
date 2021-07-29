using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AirSnitch.Api.Rest.ResponseBodyFormatters
{
    public class SimpleJsonBodyFormatter : IResponseBodyFormatter
    {
        public string FormatResponse(object responseBody)
        {
            var serializationSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.SerializeObject(responseBody, serializationSettings);
        }
    }
}