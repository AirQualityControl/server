using System;

namespace AirSnitch.Domain.Models
{
    public struct GeoCoordinates
    {
        private const int LatitudeMinValue = 0;
        private const int LatitudeMaxValue = 90;
        private const int LongitudeMinValue = 0;
        private const int LongitudeMaxValue = 180;

        private double _longitudeValue;
        private double _latitudeValue;
        private bool _isEmpty;

        /// <summary>
        /// Longitude value of the point
        /// Value shout be in range between 0 and 90 degree
        /// </summary>
        public double Longitude
        {
            get => _longitudeValue;
            set
            {
                ValidateLongitude(value);
                _longitudeValue = value;
            }
        }

        /// <summary>
        /// Latitude value of the point
        /// Value should be in range between 0 and 180
        /// </summary>
        public double Latitude
        {
            get => _latitudeValue;
            set
            {
                ValidateLatitude(value);
                _latitudeValue = value;
            }
        }

        private void ValidateLatitude(double latitudeValue)
        {
            if (!(latitudeValue > LatitudeMinValue && latitudeValue < LatitudeMaxValue))
            {
                throw new ArgumentException(
                    $"Latitude value is invalid. Value should range from 0 to 90. Received value is {latitudeValue}");
            }
        }

        private void ValidateLongitude(double longitudeValue)
        {
            if (!(longitudeValue > LongitudeMinValue && longitudeValue < LongitudeMaxValue))
            {
                throw new ArgumentException(
                    $"Longitude value is invalid. Value should range from 0 to 180. Received value is {longitudeValue}");
            }
        }
        
        public object Clone()
        {
            return new GeoCoordinates()
            {
                Longitude = this._longitudeValue,
                Latitude = this._latitudeValue
            };
        }

        public bool IsEmpty { get; set; }
        public bool IsValid()
        {
            return true;
        }

        public void Validate()
        {
            
        }
        
        public bool Equals(GeoCoordinates other)
        {
            return _longitudeValue.Equals(other._longitudeValue) 
                   && _latitudeValue.Equals(other._latitudeValue) 
                   && _isEmpty == other._isEmpty && IsEmpty == other.IsEmpty;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((GeoCoordinates) obj);
        }
    }
}