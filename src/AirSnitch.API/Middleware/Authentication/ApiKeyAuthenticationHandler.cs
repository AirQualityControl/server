using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace AirSnitch.Api.Middleware.Authentication
{
    internal class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private readonly IClientRepository _apiClientRepository;
        
        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<ApiKeyAuthenticationOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock, IClientRepository apiClientRepository) : base(options, logger, encoder, clock)
        {
            _apiClientRepository = apiClientRepository;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            StringValues headerValues;
            if (!Request.Headers.TryGetValue("X-API-KEY", out headerValues))
            {
                return AuthenticateResult.NoResult();
            }

            if (headerValues.Count > 1)
            {
                return AuthenticateResult.Fail("Multiple api key was specified in request headers");
            }

            var headerStringValue = headerValues.Single();
            
            if (!ApiKey.IsValid(headerStringValue))
            {
                return AuthenticateResult.Fail("Api key format is not valid!");
            }
            
            var apiClient = await _apiClientRepository.GetClientByApiKey(ApiKey.FromString(headerStringValue));

            if (apiClient.IsEmpty)
            {
                return AuthenticateResult.NoResult();
            }
            
            var authenticationTicket = BuildTicket(apiClient);
            
            return AuthenticateResult.Success(authenticationTicket);
        }

        private AuthenticationTicket BuildTicket(ApiClient apiClient)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, apiClient.Name.Value),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return ticket;
        }
    }
}