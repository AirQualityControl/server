using System.Globalization;

namespace AirSnitch.Api
{
    public class Constants
    {
        public static class Authentication
        {
            public static class Scheme
            {
                public const string ApiKey = "ApiKey";
            }
            
            public static class Headers
            {
                public const string ApiKey = "X-API-KEY";
            }
        }

        public static class Authorization
        {
            public static class Policy
            {
                public const string InternalApp = "InternalApp";    
            }
        }
        public static class Claim
        {
            public static string ClientId = "ClientId";
        }
    }
}