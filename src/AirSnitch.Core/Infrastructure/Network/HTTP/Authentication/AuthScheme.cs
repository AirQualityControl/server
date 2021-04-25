using RestSharp.Authenticators;

namespace AirSnitch.Core.Infrastructure.Network.HTTP.Authentication
{
    /// <summary>
    /// Abstract class that represent authentication sheme
    /// </summary>
    public abstract class AuthScheme
    {
        public abstract IAuthenticator GetAuthenticator();
    }
}