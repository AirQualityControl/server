using System;
using AirSnitch.Core.Domain.Exceptions;
using DeclarativeContracts.Functions;
using DeclarativeContracts.Precondition;

namespace AirSnitch.Core.Domain.Models
{
    /// <summary>
    /// Brief summary of user's monitoring station.
    /// </summary>
    public class AirMonitoringStationSummary : EmptyDomainModel<AirMonitoringStationSummary>, IDomainModel<AirMonitoringStationSummary>
    {
        /// <summary>
        /// Station unique identifier
        /// </summary>
        private string _id;
        public string StationId
        {
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
        /// Physical station address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Name of the city where current station is located
        /// </summary>
        public string CityName { get; set; }

        public bool Equals(AirMonitoringStationSummary other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _id == other._id;
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AirMonitoringStationSummary) obj);
        }

        public override int GetHashCode()
        {
            return (_id != null ? _id.GetHashCode() : 0);
        }

        public object Clone()
        {
            return new AirMonitoringStationSummary()
            {
                StationId = this._id,
                Address = this.Address,
                CityName = CityName
            };
        }

        public bool IsEmpty { get; set; }

        public bool IsValid()
        {
            if (String.IsNullOrEmpty(_id) && String.IsNullOrEmpty(Address))
            {
                return false;
            }

            return true;
        }

        public void Validate()
        {
            if (String.IsNullOrEmpty(_id) && String.IsNullOrEmpty(Address))
            {
                throw new InvalidEntityStateException("Air monitoring station is not valid.Eiter id or name is null or empty");
            }
        }

        public override string ToString()
        {
            return $"{_id}:{Address}";
        }
    }
}