using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication;

namespace AirSnitch.Api.Middleware.Authentication
{
    public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string Name = "ApiKey";

        public ApiKeyAuthenticationOptions()
        {
            
        }
    }
}