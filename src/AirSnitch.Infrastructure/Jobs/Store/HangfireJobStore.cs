using System.Collections.Generic;
using AirSnitch.Core.Infrastructure.Jobs;
using AirSnitch.Core.Infrastructure.JobStore;
using Hangfire;

namespace AirSnitch.Infrastructure.Jobs.Store
{
    /// <summary>
    /// Hangfire(https://www.hangfire.io/) Job Store
    /// </summary>
    public class HangfireJobStore : IJobStore
    {
        ///<inheritdoc/>
        public void AddRecurringJob(IRecurringJob recurringJob)
        {
            //Require.That(recurringJob.IsValid);

            RecurringJob.AddOrUpdate(
                recurringJobId: (string)recurringJob.Id, 
                methodCall: recurringJob.MethodToCall, 
                cronExpression:() => recurringJob.CronExpression, 
                timeZone: recurringJob.UserTimeZone
            );
        }

        ///<inheritdoc/>
        public void AddRecurringJobs(params IRecurringJob[] recurringJobs)
        {
            //Require.That(recurringJob.IsValid);
            
            foreach (var job in recurringJobs)
            {
                RecurringJob.AddOrUpdate(
                    recurringJobId: (string)job.Id, 
                    methodCall: job.MethodToCall, 
                    cronExpression:() => job.CronExpression, 
                    timeZone: job.UserTimeZone
                );
            }
        }

        public void AddRecurringJobs(ICollection<IRecurringJob> recurringJobsCollection)
        {
            //Require.That(recurringJob.IsValid);
            
            foreach (var job in recurringJobsCollection)
            {
                RecurringJob.AddOrUpdate(
                    recurringJobId: (string)job.Id, 
                    methodCall: job.MethodToCall, 
                    cronExpression:() => job.CronExpression, 
                    timeZone: job.UserTimeZone
                );
            }
        }

        ///<inheritdoc/>
        public void CancelRecurringJob(IRecurringJob job)
        {
            //Require.That(recurringJob.IsValid);
            
            string stringJobIdId = (string) job.Id;

            RecurringJob.RemoveIfExists(recurringJobId: stringJobIdId);
        }

        ///<inheritdoc/>
        public void CancelRecurringJobs(params IRecurringJob[] recurringJobs)
        {
            //Require.That(recurringJob.IsValid);
            
            foreach (var job in recurringJobs)
            {
                RecurringJob.RemoveIfExists((string)job.Id);
            }
        }

        public void CancelRecurringJobs(ICollection<IRecurringJob> recurringJobsCollection)
        {
            //Require.That(recurringJob.IsValid);
            
            foreach (var job in recurringJobsCollection)
            {
                RecurringJob.RemoveIfExists((string)job.Id);
            }
        }
    }
}