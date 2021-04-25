using System.IO;
using Newtonsoft.Json.Linq;

namespace AirSnitch.Core.Infrastructure.Configuration
{
    public class FileBasedAppConfig : IAppConfig
    {
        private const string ConfigFileName = "core_settings.json";

        //TODO: in future could be change to streaming JSON with seeking line by key.
        private static readonly JObject JsonAppConfig;

        static FileBasedAppConfig()
        {
            JsonAppConfig ??= ReadApplicationConfigJsonContent();
        }
        private static JObject ReadApplicationConfigJsonContent()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), ConfigFileName);
            return JObject.Parse(File.ReadAllText(filePath));
        }

        public string Get(string key)
        {
            return (string)JsonAppConfig[key];
        }
    }
}