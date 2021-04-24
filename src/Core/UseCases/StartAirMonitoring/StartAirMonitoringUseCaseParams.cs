using System;
using System.Collections.Generic;
using System.Linq;
using AirSnitch.Core.Domain.Models;
using AirSnitch.Core.Infrastructure.Jobs;
using AirSnitch.Core.UseCases.CancelAirMonitoring;

namespace AirSnitch.Core.UseCases.StartAirMonitoring
{
    /// <summary>
    /// 
    /// </summary>
    public class StartAirMonitoringUseCaseParams : IUseCaseParam
    {
        /// <summary>
        /// 
        /// </summary>
        public User User { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public AirMonitoringStationSummary AirMonitoringStation { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public ICollection<IRecurringJob> JobsToAdd { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public void Validate()
        {
            if (User == null || User.IsEmpty || !User.IsValid())
            {
                throw new ArgumentException(
                    "Specified param User is not valid. Please make sure that you pass a valid user value.");
            }

            if (JobsToAdd == null || !JobsToAdd.Any())
            {
                throw new ArgumentException(
                    "Jobs to add are invalid.Please make sure that specified jobs are not null or not empty");
            }

            if (AirMonitoringStation == null || AirMonitoringStation.IsEmpty || !AirMonitoringStation.IsValid())
            {
                throw new ArgumentException("AirMonitoringStation parameter is invalid.Please make sure that it is not null or not empty value");
            }
        }
    }
}