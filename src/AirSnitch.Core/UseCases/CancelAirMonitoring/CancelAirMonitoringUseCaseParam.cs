using System.Collections.Generic;
using AirSnitch.Core.Domain.Models;
using AirSnitch.Core.Infrastructure.Jobs;
using AirSnitch.Core.Infrastructure.JobStore;


namespace AirSnitch.Core.UseCases.CancelAirMonitoring
{
    public class CancelAirMonitoringUseCaseParam : IUseCaseParam
    {
        public User User { get; set; }
        public AirMonitoringStationSummary AirMonitoringStation { get; set; }
        public ICollection<IRecurringJob> JobsToCancel { get; set; }
        public void Validate()
        {
            //TODO:
        }
    }
}