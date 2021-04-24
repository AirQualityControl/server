namespace AirSnitch.Core.UseCases
{
    public class GenericUseCaseExecutionResult<T> : UseCaseExecutionResult
    {
        public T Result { get; set; }
    }
}