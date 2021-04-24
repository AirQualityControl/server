using System;
using AirSnitch.Core.Infrastructure.JobStore;

namespace AirSnitch.Core.Infrastructure.Jobs
{
    /// <summary>
    /// Interface that represent job that will be scheduled periodically
    /// </summary>
    public interface IRecurringJob : IJob
    {
        /// <summary>
        /// Unique identifier of the job
        /// </summary>
        object Id { get;}
        
        /// <summary>
        /// User time zone that represent in which timezone execute cron expression
        /// </summary>
        TimeZoneInfo UserTimeZone { get; }

        /// <summary>
        /// Cron expression that represent period of job execution
        /// </summary>
        string CronExpression { get;}
    }
}