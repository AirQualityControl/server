using System.Threading.Tasks;
using AirSnitch.Core.Infrastructure.JobStore;
using AirSnitch.Core.Infrastructure.Persistence;
using DeclarativeContracts.Functions;
using DeclarativeContracts.Precondition;

namespace AirSnitch.Core.UseCases.CancelAirMonitoring
{
    public class CancelAirMonitoringUseCase : ICancelAirMonitoringUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJobStore _jobStore;

        public CancelAirMonitoringUseCase(IUserRepository userRepository, 
            IJobStore jobStore)
        {
            _userRepository = userRepository;
            _jobStore = jobStore;
        }
        
        public async Task<UseCaseExecutionResult> ExecuteAsync(CancelAirMonitoringUseCaseParam useCaseParam)
        {
            useCaseParam.Validate();
            
            var user = useCaseParam.User;
            
            user.CancelAirMonitoringForStation(useCaseParam.AirMonitoringStation);
            
            await _userRepository.Update(user);
            
            _jobStore.CancelRecurringJobs(useCaseParam.JobsToCancel);

            return await Task.FromResult(new UseCaseExecutionResult {IsSuccess = true});
        }
    }
}