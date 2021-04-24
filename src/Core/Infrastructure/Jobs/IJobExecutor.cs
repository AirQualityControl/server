namespace AirSnitch.Core.Infrastructure.JobStore
{
    /// <summary>
    /// Interface that represent a various types of job execution
    /// </summary>
    public interface IJobExecutor
    {
        /// <summary>
        /// Execute job according to concrete implementation.
        /// For instance, background job store will execute this job in background.
        /// </summary>
        /// <param name="job"></param>
        void ExecuteJob(IJob job);
    }
}