namespace AirSnitch.Domain.Models
{
    public class Location
    {
        public string GetAddress()
        {
            return "";
        }

        public City GetCity()
        {
            return null;
        }

        public Country GetCountry()
        {
            return null;
        }

        public GeoCoordinates GeoCoordinates()
        {
            return default(GeoCoordinates);
        }
    }
}