using System;
using System.Linq.Expressions;

namespace AirSnitch.Core.Infrastructure.JobStore
{
    /// <summary>
    /// Represent abstract void job with a certain state.
    /// All other jobs is inherit from this class such as a BackgroundJob, recurringJob
    /// </summary>
    public interface IJob
    {
        /// <summary>
        /// Reference to method expression that will called during job execution
        /// </summary>
        Expression<Action> MethodToCall { get;}
        
        /// <summary>
        /// Verify that current state of a job is valid
        /// </summary>
        bool IsValid();
    }
}