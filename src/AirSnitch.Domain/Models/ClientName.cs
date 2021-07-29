namespace AirSnitch.Domain.Models
{
    public class ClientName
    {
        private readonly string _clientName;
        
        public ClientName(string name)
        {
            _clientName = name;
        }

        public string Value => _clientName;
    }
}