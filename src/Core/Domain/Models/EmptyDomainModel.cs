namespace AirSnitch.Core.Domain.Models
{
    public class EmptyDomainModel<T> where T : IDomainModel<T>, new()
    {
        public static T Empty { get; } = new T {IsEmpty = true};
    }
}