namespace AirSnitch.Domain.Models
{
    public class ClientDescription
    {
        private readonly string _description;
        
        public ClientDescription(string description)
        {
            _description = description;
        }

        public string Value => _description;
    }
}