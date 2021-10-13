using AirSnitch.Api.Middleware.Authentication;
using Microsoft.AspNetCore.Authentication;

namespace AirSnitch.Api.Extensions
{
    public static class ApikeyAuthExtensions
    {
        public static AuthenticationBuilder AddApiKey(this AuthenticationBuilder builder)
        {
            return builder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(
                ApiKeyAuthenticationOptions.Name, 
                null
            );
        }
    }
}