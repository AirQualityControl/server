using System.Threading.Tasks;
using AirSnitch.Core.Domain.Models;
using AirSnitch.Core.Infrastructure.JobStore;
using AirSnitch.Core.Infrastructure.Persistence;

namespace AirSnitch.Core.UseCases.StartAirMonitoring
{
    public class StartAirMonitoringUseCase : IStartAirMonitoringUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJobStore _jobStore;
        
        public StartAirMonitoringUseCase(IUserRepository userRepository, IJobStore jobStore)
        {
            _userRepository = userRepository;
            _jobStore = jobStore;
        }
        
        public Task<GenericUseCaseExecutionResult<StartAirMonitoringRequestResult>> ExecuteAsync(StartAirMonitoringUseCaseParams useCaseParam)
        {
            useCaseParam.Validate();
            
            var user = useCaseParam.User;
            var airMonitoringResult = user.StartAirMonitoringForStation(useCaseParam.AirMonitoringStation);
            if (airMonitoringResult.IsSuccess)
            {
                _userRepository.Update(user);
                _jobStore.AddRecurringJobs(useCaseParam.JobsToAdd);
                return Task.FromResult(new GenericUseCaseExecutionResult<StartAirMonitoringRequestResult>()
                {
                    IsSuccess = true,
                    Result = airMonitoringResult,
                });
            }
            return Task.FromResult(new GenericUseCaseExecutionResult<StartAirMonitoringRequestResult>()
            {
                IsSuccess = false,
                Result = airMonitoringResult,
            });
        }
    }
}