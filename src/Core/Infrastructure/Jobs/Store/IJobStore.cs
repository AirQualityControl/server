using System.Collections.Generic;
using AirSnitch.Core.Infrastructure.Jobs;

namespace AirSnitch.Core.Infrastructure.JobStore
{
    /// <summary>
    ///     Interface that represent generic job store.
    /// </summary>
    public interface IJobStore
    {
        /// <summary>
        ///    Add recurring job to persistent store
        /// </summary>
        /// <throws>ContractViolationException if recurring job is not valid</throws>
        /// Precondition: recurringJob job should be valid.
        /// <param name="recurringJob"></param>
        void AddRecurringJob(IRecurringJob recurringJob);
        
        /// <summary>
        ///    Add recurring jobs to persistent store.
        /// </summary>
        /// <throws>ContractViolationException if recurring job is not valid</throws>
        /// Precondition: recurringJob job should be valid.
        /// <param name="recurringJobs">params ob jobs to be canceled</param>
        void AddRecurringJobs(params IRecurringJob[] recurringJobs);
        
        /// <summary>
        ///    Add recurring jobs to persistent store.
        /// </summary>
        /// <throws>ContractViolationException if recurring job is not valid</throws>
        /// Precondition: recurringJob job should be valid.
        /// <param name="recurringJobsCollection">Collection of recurring jobs to be canceled</param>
        void AddRecurringJobs(ICollection<IRecurringJob> recurringJobsCollection);
        
        /// <summary>
        ///    Cancel scheduled execution of recurring job.Does not delete record from a store
        /// </summary>
        /// <throws>ContractViolationException if recurring job is not valid</throws>
        /// Precondition: recurringJob job should be valid.
        /// <param name="job">Job to be cancel</param>
        void CancelRecurringJob(IRecurringJob job);

        /// <summary>
        ///    Cancel scheduled execution of recurring jobs.Does not delete records from a store
        /// </summary>
        /// <throws>ContractViolationException if recurring job is not valid</throws>
        /// Precondition: recurringJob job should be valid.
        /// <param name="recurringJobs">params ob jobs to be canceled</param>
        void CancelRecurringJobs(params IRecurringJob[] recurringJobs);
        
        /// <summary>
        ///    Cancel scheduled execution of recurring jobs.Does not delete records from a store
        /// </summary>
        /// <throws>ContractViolationException if recurring job is not valid</throws>
        /// Precondition: recurringJob job should be valid.
        /// <param name="recurringJobsCollection">Collection of recurring jobs to be canceled</param>
        void CancelRecurringJobs(ICollection<IRecurringJob> recurringJobsCollection);
    }
}