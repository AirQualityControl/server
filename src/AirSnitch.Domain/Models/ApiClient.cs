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

        public void GenerateId()
        {
            _id = Guid.NewGuid().ToString();
        }

        public string Id => _id;
        public ClientName Name {get; set;}
        public ClientDescription Description {get; set; }
        public DateTime CreatedOn {get; set; }
        public ClientType Type {get; set; }
        public ClientStatus Status { get; } = ClientStatus.Active;

        public void Activate()
        {

        }

        public void Deactive()
        {

        }

        public void SetState(ApiClient clientState)
        {
            this.Name = clientState.Name;
            this.Description = clientState.Description;
            this.Type = ClientType.Production;
        }

        public bool Equals(ApiClient? other)
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public bool IsEmpty { get; set; }
        public static ApiClient Empty => new ApiClient() {IsEmpty = true};

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

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