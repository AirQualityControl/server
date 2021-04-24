using System;
using AirSnitch.Core.Domain.Exceptions;

namespace AirSnitch.Core.Domain.Models
{
    /// <summary>
    /// Object that represent city where air monitoring stations is located
    /// Also air pollution could be obtained by the city itself
    /// </summary>
    public class City: EmptyDomainModel<City>, IDomainModel<City>
    {
        private const string CityCompositeKeyTemplate = "{0}_{1}_{2}";
        
        public City()
        {
           
        }

        /// <summary>
        /// Friendly name of the city for end users
        /// </summary>
        public string FriendlyName { get; set; }
        
        /// <summary>
        /// City code.(Unique combination of letters)
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// State of particular city.For instance:
        /// city: Brovary
        /// state: Kyivska oblast
        /// </summary>
        public string State { get; set; }
        
        /// <summary>
        /// Unique code of the country
        /// </summary>
        public string CountryCode { get; set; }
        
        /// <summary>
        /// Composite key that uniquely characterize particular City.
        /// This key comprises of : city code, city state and country code.
        /// </summary>
        public string CompositeKey => InternalGenerateCompositeKey(Code, State, CountryCode);

        private string InternalGenerateCompositeKey(string cityName, string cityState, string cityCountry) 
        {
            return String.Format(format: CityCompositeKeyTemplate, cityName, cityState, cityCountry);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((City) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FriendlyName, Code, State, CountryCode);
        }
        
        
        public bool Equals(City other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return FriendlyName == other.FriendlyName && Code == other.Code && State == other.State && CountryCode == other.CountryCode;
        }

        public override string ToString()
        {
            return FriendlyName;
        }

        public object Clone()
        {
            return new City()
            {
                FriendlyName = this.FriendlyName,
                State = this.State,
                Code = this.Code,
                CountryCode = this.CountryCode
            };
        }

        public bool IsEmpty { get; set; }
        public bool IsValid()
        {
            if (String.IsNullOrEmpty(Code) && String.IsNullOrEmpty(CountryCode) && String.IsNullOrEmpty(FriendlyName))
            {
                return false;
            }
            return true;
        }

        public void Validate()
        {
            if (String.IsNullOrEmpty(Code) && String.IsNullOrEmpty(CountryCode) && String.IsNullOrEmpty(FriendlyName))
            {
                throw new InvalidEntityStateException("City code countryCode and friendly name should be field in");
            }
        }
    }
}