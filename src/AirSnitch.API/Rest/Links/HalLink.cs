using Newtonsoft.Json.Linq;

namespace AirSnitch.Api.Controllers
{
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