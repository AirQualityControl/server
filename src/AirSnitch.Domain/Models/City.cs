namespace AirSnitch.Domain.Models
{
    public class City
    {
        private City()
        {
            
        }

        public string Name { get; private set; }

        public string Code { get; private set; }
        
        public static City FromString(string cityString)
        {
            return new City()
            {
                Name = "Kyiv",
                Code = "IEV"
            };
        }
    }
}