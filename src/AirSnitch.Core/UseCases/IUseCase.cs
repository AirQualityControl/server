using System.Threading.Tasks;

namespace AirSnitch.Core.UseCases
{
    /// <summary>
    /// Interface that represent a core system use case that is common for any client.
    /// </summary>
    public interface IUseCase<in TUseCaseParam>
    {
        Task<UseCaseExecutionResult> ExecuteAsync(TUseCaseParam useCaseParam);
    }
}