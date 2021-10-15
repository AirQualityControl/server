using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication;

namespace AirSnitch.Api.Middleware.Authentication
{
    public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        public ApiKeyAuthenticationOptions()
        {
            
        }
    }
}