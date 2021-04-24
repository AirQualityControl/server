using System;


namespace AirSnitch.Core.Infrastructure.Configuration
{
    public class EnvironmentVariablesConfig : IAppConfig
    {
        public string Get(string key)
        {
            var variableValue = Environment.GetEnvironmentVariable(key);
            return variableValue;
        }
    }
}