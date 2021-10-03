using System;

namespace AirSnitch.Domain.Models
{
    public class ApiClient
    {
        private readonly string _id;
        public ApiClient(string id)
        {
            _id = id;
        }
        public string Id => _id;
        public ClientName Name {get; set;}
        public ClientDescription Description {get; set; }
        public DateTime CreatedOn {get; set; }
        public ClientType Type {get; set; }
        public ClientStatus Status {get; set; }

        public void Activate()
        {

        }

        public void Deactive()
        {

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