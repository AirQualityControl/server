using System;
using System.Threading.Tasks;
using AirSnitch.Core.Domain.Exceptions;
using DeclarativeContracts.Functions;
using Ensure = DeclarativeContracts.Postcondition.Ensure;
using Require = DeclarativeContracts.Precondition.Require;

namespace AirSnitch.Core.Domain.Models
{
    /// <summary>
    /// Air monitoring station the emmit data about air pollution
    /// </summary>
    public class AirMonitoringStation : EmptyDomainModel<AirMonitoringStation>, IDomainModel<AirMonitoringStation>
    {
        private string _id;
        
        public AirMonitoringStation()
        {
            
        }
        
        private readonly IAirPollutionDataProvider _dataProvider;
        public AirMonitoringStation(IAirPollutionDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        /// <summary>
        /// Unique identifier of station.
        /// Value cannot be null or empty string
        ///<throws>ArgumentException</throws>
        /// </summary>
        public string Id {
            get => _id;
            set
            {
                Require.That(
                    element: value, 
                    predicate: Is.NotNullOrEmptyString, 
                    exceptionToThrow: new ArgumentException("Invalid monitoring station id value was specified")
                );
                _id = value;
            }
        }

        /// <summary>
        /// Name of station assigned by station provider
        /// Value cannot be null or empty string
        ///<throws>ArgumentException</throws>
        /// </summary>
        private string _name;
        public string Name {
            get => _name;
            set
            {
                Require.That(
                    element: value, 
                    predicate: Is.NotNullOrEmptyString, 
                    exceptionToThrow: new ArgumentException("Invalid monitoring station Name value was specified")
                );
                _name = value;
            }
        }
        
        /// <summary>
        /// Station name that characterize it location. For instance street address
        /// </summary>
        public string LocalName { get; set;}
        
        /// <summary>
        /// Market that states whether station is active or not.
        /// Keep in mind that this property could be changes during a day
        /// depending on physical station state
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Station time zone
        /// </summary>
        public string TimeZone {get;set;}

        /// <summary>
        /// City <see cref="Models.City"/>  where current station is located
        /// Value cannot be null
        ///<throws>ArgumentException</throws>
        /// </summary>
        
        private City _city;
        public City City {
            get => _city;
            set
            {
                Require.That(
                    element: value,
                    predicate:Is.NotNull,
                    exceptionToThrow:new ArgumentException("Invalid value for city was specified: city cannot be NULL")
                );
                _city = value;
            }
        }
        
        /// <summary>
        /// Concrete geolocation coordinate of monitoring station
        /// Value cannot be null and should represent valid GeoLocation <see cref="GeoLocation"/>
        ///<throws>ArgumentException</throws>
        /// </summary>
        public GeoLocation Location { get; set; }

        /// <summary>
        /// Method that return short summary <see cref="AirMonitoringStationSummary"/> for current station
        /// </summary>
        /// <returns></returns>
        public AirMonitoringStationSummary GetSummary()
        {
            return new AirMonitoringStationSummary()
            {
                StationId = Id,
                Address = Name,
                CityName = City.FriendlyName,
            };
        }

        /// <summary>
        /// Meta information about data provider
        /// </summary>
        public AirPollutionDataProvider DataProvider { get; set; }

        /// <summary>
        /// Returns air pollution data from station
        /// </summary>
        /// <returns></returns>
        public async Task<AirPollution> GetLatestAirPollutionAsync()
        {
            Require.That(_dataProvider, Is.NotNull);
            var airPollutionData = await _dataProvider.GetLatestDataAsync(station:this);
            Ensure.That(_dataProvider, Is.NotNull);
            airPollutionData.MonitoringStation = this;
            
            if (IsAirPollutionDataActual(airPollutionData))
            {
                return airPollutionData;
            }
            
            return await Task.FromResult(AirPollution.Empty);
        }

        private bool IsAirPollutionDataActual(AirPollution airPollution)
        {
            var now = DateTime.Now;
            var twoHours = TimeSpan.FromHours(3);
            return now - airPollution.MeasurementDateTime <= twoHours;
        }

        /// <summary>
        /// Perform a deep copy of monitoring station
        /// </summary>
        /// <returns>Deep copy of selected station</returns>
        public object Clone()
        {
            return new AirMonitoringStation(_dataProvider)
            {
                Id = Id,
                Name = this.Name,
                LocalName = this.LocalName,
                IsActive = IsActive,
                TimeZone = TimeZone,
                City = new City()
                {
                    Code = City.Code,
                    CountryCode = City.CountryCode,
                    FriendlyName = City.FriendlyName,
                    State = City.State
                },
                Location = new GeoLocation()
                {
                    Latitude = Location.Latitude,
                    Longitude = Location.Longitude
                }
            };
        }


        public bool IsEmpty { get; set; }
        
        ///<inheritdoc/>
        public bool IsValid()
        {
            if (String.IsNullOrEmpty(Id) && String.IsNullOrEmpty(Name))
            {
                return false;
            }

            return true;
        }

        ///<inheritdoc/>
        public void Validate()
        {
            if (String.IsNullOrEmpty(Id) && String.IsNullOrEmpty(Name))
            {
                throw new InvalidEntityStateException("Air monitoring station is not valid.Eiter id or name is null or empty");
            }
        }


        public bool Equals(AirMonitoringStation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _id == other._id;
        }

        public override int GetHashCode()
        {
            return (_id != null ? _id.GetHashCode() : 0);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AirMonitoringStation) obj);
        }

        public override string ToString()
        {
            return $"{Id}:{Name}:{LocalName}";
        }
    }
}