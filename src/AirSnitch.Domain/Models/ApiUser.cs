using System;
using System.Collections.Generic;
using System.Linq;
using DeclarativeContracts.Functions;
using DeclarativeContracts.Precondition;

namespace AirSnitch.Domain.Models
{
    /// <summary>
    /// ADT that represent an Api user.
    /// </summary>
    public class ApiUser : IDomainModel<ApiUser>
    {
        private List<ApiClient> _clients = new List<ApiClient>();
        
        public ApiUser() { }

        public ApiUser(string id)
        {
            Id = id;
        }

        public string Id { get; }

        public string PrimaryKey { get; set; }

        /// <summary>
        /// Returns a profile information of current user.
        /// For more details what is profile take a look at 
        /// </summary>
        public ApiUserProfile Profile { get; set; }
        public IReadOnlyCollection<ApiClient> Clients => _clients;
        public SubscriptionPlan SubscriptionPlan { get; private set; }
        public void PromoteSubscriptionPlan()
        {
            throw new NotImplementedException();
        }
        public void DowngradeSubscriptionPlan()
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        ///     Method that adds client for specific api user
        ///
        ///     Pre-conditions: require that apiClientId is not null or empty string, and value is a valid client identifier
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(ApiClient client)
        {
            _clients.Add(client);
        }
        
        public void SetSubscriptionPlan(SubscriptionPlan subscriptionPlan)
        {
            this.SubscriptionPlan = subscriptionPlan;
        }
        
        /// <summary>
        ///     Methods that remove a specified client by id for current user.
        ///     If client with specified id does not belongs to current instance - RemovalResult.NotFound will be returned,
        ///     otherwise RemovalResult.Success will be returned
        ///
        ///     Pre-conditions: require that apiClientId is not null or empty string, and value is a valid client identifier
        ///     Post-condition: not empty removal result returns.
        /// </summary>
        /// <param name="apiClientId">Valid apiClientIdentified</param>
        /// <returns>Removal result</returns>
        public RemovalResult RemoveClientById(string apiClientId)
        {
            Require.That(apiClientId, Is.NotNullOrEmptyString);

            var targetClient = _clients.FirstOrDefault(c => c.Id.Equals(apiClientId));

            if (targetClient == null)
            {
                return RemovalResult.NotFound;
            }

            _clients.Remove(targetClient);
            
            return RemovalResult.Success;
        }
        
        /// <summary>
        ///     
        /// </summary>
        /// <param name="client"></param>
        /// <exception cref="NotImplementedException"></exception>
        public RemovalResult RemoveClient(ApiClient client)
        {
            Require.That(client, Is.NotNull);

            if (_clients.Contains(client))
            {
                _clients.Remove(client);
                return RemovalResult.Success;
            }
            return RemovalResult.NotFound;
        }
        
        /// <summary>
        ///     Methods that removes all client from current ApiUser
        /// </summary>
        public void RemoveAllClients()
        {
            _clients = new List<ApiClient>();
        }
        
        public void Block()
        {
            throw new NotImplementedException();
        }
        
        public void UnBlock()
        {
            throw new NotImplementedException();
        }
        
        public bool Equals(ApiUser other)
        {
            throw new System.NotImplementedException();
        }
        
        public object Clone()
        {
            throw new System.NotImplementedException();
        }
        public bool IsEmpty { get; set; }
        public static ApiUser Empty { get; set; }
        public bool IsValid()
        {
            if (Clients == null) return false;

            if (Profile == null) return false;

            if (SubscriptionPlan == null) return false;

            return true;
        }
        public void Validate()
        {
            if (Clients == null)
            {
                throw new InvalidEntityStateException(
                    "Clients property could not be null and should be empty collection instead");
            }

            if (Profile == null)
            {
                throw new InvalidEntityStateException("Profile property could not be null. User profile empty insted");
            }

            if (SubscriptionPlan == null)
            {
                throw new InvalidEntityStateException("Subscription plan could not be null. Use empty insted");
            }
        }
    }

    public enum RemovalResult
    {
        Success,
        NotFound
    }
}