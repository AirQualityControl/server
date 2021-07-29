using System;

namespace AirSnitch.Domain.Models
{
    /// <summary>
    ///     Interface that represent a common contract that every domain model should follow.
    /// </summary>
    /// <typeparam name="T">Type of domain model</typeparam>
    public interface IDomainModel<T> : IEquatable<T>, ICloneable where T: new()
    {
        /// <summary>
        ///     Define weather domain model is empty or not
        /// </summary>
        bool IsEmpty { get; set; }
        
        /// <summary>
        ///     Method that checks if domain model state is valid or not.
        ///     For instance state is not valid if some required filed
        ///     of domain model is not set.
        ///     Never throws and exception
        /// </summary>
        /// <returns>True in case domain model valid, otherwise false</returns>
        bool IsValid();

        /// <summary>
        ///     Method that checks if domain model state is valid or not.
        ///     If state is not valid InvalidModelStateException will be thrown
        /// </summary>
        /// <throws>InvalidModelStateException</throws>
        void Validate();
    }
}