using System;
using System.Diagnostics.Contracts;


using RestSharp.Authenticators;

namespace AirSnitch.Core.Infrastructure.Network.HTTP.Authentication
{
    /// <summary>
    /// The "Basic" HTTP authentication scheme is defined in RFC 7617,
    /// which transmits credentials as user ID/password pairs, encoded using base64.
    /// </summary>
    public class BasicAuthScheme : AuthScheme
    {
        public BasicAuthScheme(string userName, string userPassword)
        {
            UserName = userName;
            UserPassword = userPassword;
        }
        
        private string _userName;
        private string UserName
        {
            get => _userName;
            set
            {
                Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(value),
                    $"user name should be not null and not an empty string.Actual value is {value}");
                _userName = value;
            }
        }
        
        private string _userPassword;
        private string UserPassword {
            get => _userPassword;
            set
            {
                Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(value),
                    $"user name should be not null and not an empty string.Actual value is {value}");
                _userPassword = value;
            }
        }

        public override IAuthenticator GetAuthenticator()
        {
            return new HttpBasicAuthenticator(UserName, UserPassword);
        }
    }
}