namespace AirSnitch.Domain.Models
{
    public class MonitoringStation : IDomainModel<MonitoringStation>
    {
        public string Id { get; }

        public string DisplayName { get; }


        public Location GetLocation()
        {
            return null;
        }

        /// <summary>
        ///     Returns a latest air pollution value from current station
        /// </summary>
        /// <returns>Valid air pollution measurements</returns>
        public AirPollution GetAirPollution()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        ///     Returns a monitoring station owner.
        /// </summary>
        /// <returns></returns>
        public MonitoringStationOwner GetOwner()
        {
            throw new System.NotImplementedException();
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
        
        public bool IsValid()
        {
            throw new System.NotImplementedException();
        }

        public void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}