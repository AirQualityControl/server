namespace AirSnitch.Core.Infrastructure.Configuration
{
    public static class AppConfigurationFactory
    {
        public static IAppConfig GetAppConfig()
        {
            #if DEBUG
                return new FileBasedAppConfig();
            #else
                return new EnvironmentVariablesConfig(); 
            #endif
        }
    }
}