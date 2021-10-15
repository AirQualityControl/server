namespace AirSnitch.Api
{
    public class Constants
    {
        public static class Authentication
        {
            public const string SchemeName = "ApiKey";
            
            public static class Headers
            {
                public const string ApiKey = "X-API-KEY";
            }
        }

        public static class Authorization
        {
            public const string InternalAppPolicyName = "InternalApp";    
        }
        public static class Claim
        {
            public static string ClientId = "ClientId";
        }
    }
}