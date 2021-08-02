using System;
using System.Collections.Generic;

namespace AirSnitch.Domain.Models
{
    /// <summary>
    /// ADT that represent an Api user.
    /// </summary>
    public class ApiUser : IDomainModel<ApiUser>
    {
        public ApiUser()
        {
            
        }

        /// <summary>
        /// Returns a profile information of current user.
        /// For more details what is profile take a look at 
        /// </summary>
        public ApiUserProfile Profile { get; set; }

        public IReadOnlyCollection<ApiClient> Clients { get; set; }

        public SubscriptionPlan SubscriptionPlan { get; set; }

        public void PromoteSubscriptionPlan()
        {
            throw new NotImplementedException();
        }

        public void DowngradeSubscriptionPlan()
        {
            throw new NotImplementedException();
        }

        public void AddClient()
        {
            throw new NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}