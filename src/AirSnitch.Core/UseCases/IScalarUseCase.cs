using System.Threading.Tasks;

namespace AirSnitch.Core.UseCases
{
    public interface IScalarUseCase<TUseCaseResult, in TUseCaseParam>
    {
        Task<GenericUseCaseExecutionResult<TUseCaseResult>> ExecuteAsync(TUseCaseParam useCaseParam);
    }
}