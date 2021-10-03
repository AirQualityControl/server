using System;
using System.Collections.Generic;

namespace AirSnitch.Domain.Models
{
    /// <summary>
    /// ADT that represent an Api user.
    /// </summary>
    public class ApiUser : IDomainModel<ApiUser>
    {
        private readonly List<ApiClient> _clients = new List<ApiClient>();
        
        public ApiUser() { }

        public ApiUser(string id)
        {
            Id = id;
        }

        public string Id { get; }

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
        public void AddClient(ApiClient client)
        {
            _clients.Add(client);
        }
        public void SetSubscriptionPlan(SubscriptionPlan subscriptionPlan)
        {
            this.SubscriptionPlan = subscriptionPlan;
        }
        public void RemoveClientById(string apiClientId)
        {
            
        }
        public void RemoveClient(ApiClient client)
        {
            throw new NotImplementedException();
        }
        public void RemoveAllClients()
        {
            
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
}