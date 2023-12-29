using System;

namespace AirSnitch.Domain.Models
{
    public class MonitoringStation : IDomainModel<MonitoringStation>
    {
        private static readonly MonitoringStation EmptyMonitoringStation = new MonitoringStation() { IsEmpty = true };
        private AirPollution _airPollution;
        private Location _location;
        private MonitoringStationOwner _owner;

        public MonitoringStation()
        {
            Id = GenerateId();
        }

        public MonitoringStation(string id)
        {
            Id = id;
        }

        /// <summary>
        ///     Returns an internal identifier of monitoring station.
        /// </summary>
        public string Id { get; set; }

        public string PrimaryKey { get; set; }

        /// <summary>
        ///     Returns station display name
        /// </summary>
        public string DisplayName { get; private set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        internal void SetLocation(Location location)
        {
            _location = location;
        }

        public Location GetLocation()
        {
            return _location;
        }

        /// <summary>
        ///     Returns a latest air pollution value from current station 
        /// </summary>
        /// <returns>Valid air pollution measurements</returns>
        public AirPollution GetAirPollution()
        {
            return _airPollution;
        }
        
        /// <summary>
        ///     Set the latest air pollution values that was collection from station
        /// </summary>
        /// <param name="airPollution"></param>
        internal void SetAirPollution(AirPollution airPollution)
        {
            _airPollution = airPollution;
        }
        
        /// <summary>
        ///     Returns a monitoring station owner.
        /// </summary>
        /// <returns></returns>
        public MonitoringStationOwner GetStationOwner()
        {
            return _owner;
        }
        
        public bool Equals(MonitoringStation? other)
        {
            throw new System.NotImplementedException();
        }

        public object Clone()
        {
            throw new System.NotImplementedException();
        }

        public bool IsEmpty { get; set; }

        public static MonitoringStation Empty => EmptyMonitoringStation;

        public bool IsValid()
        {
            return true;
        }

        public void Validate()
        {
            
        }
        
        internal void SetName(string name)
        {
            //TODO:check contracts
            DisplayName = name;
        }

        internal void SetOwnerInfo(MonitoringStationOwner owner)
        {
            _owner = owner;
        }
        
        private string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }

        public bool HasActualData()
        {
            var measurementsDateInUtc = GetAirPollution().GetMeasurementsDateTime();

            return DateTime.UtcNow - measurementsDateInUtc <= TimeSpan.FromHours(2);
        }
    }
}