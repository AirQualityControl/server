using System;
using System.Linq;
using System.Threading.Tasks;
using AirSnitch.Core.Infrastructure.JobStore;
using AirSnitch.Core.Infrastructure.Logging;
using AirSnitch.Core.Infrastructure.Persistence;

namespace AirSnitch.Core.UseCases.UserBlocksAppUseCase
{
    //TODO: in future remove from core
    public class UserBlockedAppUseCase : IUserBlockedAppUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJobStore _jobStore;
        private readonly ILog _logger;

        public UserBlockedAppUseCase(
            IUserRepository userRepository, 
            IJobStore jobStore, 
            ILog logger)
        {
            _userRepository = userRepository;
            _jobStore = jobStore;
            _logger = logger;
        }

        public Task<UseCaseExecutionResult> ExecuteAsync(UserBlockedClientUseCaseParams useCaseParam)
        {
            _logger.Info("Start execution deactivation use-case");
            useCaseParam.Validate();
            
            var user = useCaseParam.User;
            
            user.Deactivate();

            try
            {
                _userRepository.Update(user);
                _logger.Info($"user {user} record was deactivated successfully");
                foreach (var station in user.AirMonitoringStations)
                {
                    _jobStore.CancelRecurringJobs(useCaseParam.JobsToCancel);
                    _logger.Info($"Jobs for user {user}  and station {station} was canceled");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(new
                {
                    Text = $"Error occured during {nameof(UserBlockedAppUseCase)} execution",
                    Exception = ex,
                });
                return Task.FromResult(new UseCaseExecutionResult() {IsSuccess = false});
            }
            
            return Task.FromResult(new UseCaseExecutionResult {IsSuccess = true});
        }
    }
}