using AirSnitch.Core.Infrastructure.JobStore;
using Hangfire;

namespace AirSnitch.Core.Infrastructure.Jobs.Background
{
    /// <summary>
    /// Execute jobs in background and in separate thread pool.
    /// Current implementation is based on HangFire approach
    /// to background job execution. For more details please
    /// check out the following link.
    /// https://docs.hangfire.io/en/latest/background-methods/index.html
    /// </summary>
    public class BackgroundJobExecutor : IJobExecutor
    {
        /// <summary>
        /// Execute job in background
        /// </summary>
        /// <param name="job"></param>
        public void ExecuteJob(IJob job)
        {
            if (job.IsValid())
            {
                BackgroundJob.Enqueue(job.MethodToCall);
            }
        }
    }
}