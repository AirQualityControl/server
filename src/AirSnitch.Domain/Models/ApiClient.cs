using System;

namespace AirSnitch.Domain.Models
{
    public class ApiClient : IDomainModel<ApiClient>
    {
        private string _id;

        public ApiClient()
        {
            
        }
        
        public ApiClient(string id)
        {
            _id = id;
        }

        /// <summary>
        /// Generate a unique identifier for current api client
        /// </summary>
        public void GenerateId()
        {
            _id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Returns a unique identifier of current api client
        /// </summary>
        public string Id => _id;
        
        /// <summary>
        /// Client name
        /// </summary>
        public ClientName Name { get; set;}
        
        /// <summary>
        /// Client description
        /// </summary>
        public ClientDescription Description { get; set; }
        
        /// <summary>
        /// Date when client was created
        /// </summary>
        public DateTime CreatedOn { get; set; }
        
        /// <summary>
        /// Client type: testing or production.For more details see ClientType enumeration
        /// </summary>
        public ClientType Type { get; set; }

        /// <summary>
        /// Return an api key for current client.In case if client does not have api key ApiKey.Empty will be returned
        /// </summary>
        public ApiKey ApiKey { get; set; }

        /// <summary>
        /// Returns status of current client. Active or banned
        /// </summary>
        public ClientStatus Status { get; private set; } = ClientStatus.Active;

        /// <summary>
        /// Method that activate current client
        /// </summary>
        public void Activate()
        {
            Status = ClientStatus.Active;
        }

        /// <summary>
        /// Method that deactivate current client
        /// </summary>
        public void DeActive()
        {
            Status = ClientStatus.BLocked;
        }

        /// <summary>
        /// Method that revoke api key for current client
        /// </summary>
        public void RevokeApikey()
        {
            ApiKey = ApiKey.Empty;
        }

        /// <summary>
        /// Method that generate an new api key for current client
        /// </summary>
        public ApiKey RegenerateApiKey()
        {
            ApiKey = ApiKey.Generate();
            return ApiKey;
        }

        /// <summary>
        /// Set a new, state for current instance
        /// </summary>
        /// <param name="clientState"></param>
        public void SetState(ApiClient clientState)
        {
            Name = clientState.Name;
            Description = clientState.Description;
            Type = ClientType.Production;
        }

        public bool Equals(ApiClient? other)
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cheks wether current instance is empty or not  
        /// </summary>
        public bool IsEmpty { get; set; }
        
        /// <summary>
        /// Returns an empty instance of ApiClient
        /// </summary>
        public static ApiClient Empty => new ApiClient() {IsEmpty = true};

        /// <summary>
        /// Checks whether current api client instance is valid or not
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Validate current api client instance
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Validate()
        {
            throw new NotImplementedException();
        }
    }

    public enum ClientStatus
    {
        BLocked,
        Active
    }

    public enum ClientType
    {
        Testing,
        Production
    }
}