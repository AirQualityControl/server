using System;

namespace AirSnitch.Core.Domain.Exceptions
{
    public class AirMonitoringStationNotFoundException : Exception
    {
        private new const string Message =
            "Air monitoring station not found! Please make sure that supplied id is correct!";
        
        public AirMonitoringStationNotFoundException()
        {
            
        }

        public AirMonitoringStationNotFoundException(string message = Message) : base(message)
        {
            
        }

        public AirMonitoringStationNotFoundException(string message, Exception ex) : base(message, ex)
        {
            
        }
    }
}