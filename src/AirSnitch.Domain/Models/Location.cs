namespace AirSnitch.Domain.Models
{
    public class Location
    {
        private City _city;
        private Country _country;
        private GeoCoordinates _geoCoordinates;
        private string _addressValue;


        public string GetAddress()
        {
            return _addressValue;
        }

        public City GetCity()
        {
            return _city;
        }

        public Country GetCountry()
        {
            return _country;
        }

        public GeoCoordinates GeoCoordinates()
        {
            return _geoCoordinates;
        }

        internal void SetGeoCoordinates(GeoCoordinates coordinates)
        {
            _geoCoordinates = coordinates;
        }

        internal void SetCountry(Country country)
        {
            _country = country;
        }

        internal void SetCity(City city)
        {
            _city = city;
        }

        internal void SetAddress(string addressValue)
        {
            _addressValue = addressValue;
        }
    }
}