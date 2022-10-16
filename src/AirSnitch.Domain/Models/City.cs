namespace AirSnitch.Domain.Models
{
    public class City
    {
        private readonly string _name;
        private readonly string _code;

        private City()
        {
            
        }
        
        internal City(string name, string code = "N/A")
        {
            _name = name;
            _code = code;
        }

        public string Name => _name;

        public string Code => _code;
    }
}