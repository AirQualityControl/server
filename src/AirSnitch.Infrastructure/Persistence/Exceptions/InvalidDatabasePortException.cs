using System;

namespace AirSnitch.Infrastructure.Persistence.Exceptions
{
    /// <summary>
    ///     Represent an exception that occured when provided database port value is invalid
    /// </summary>
    public class InvalidDatabasePortException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the InvalidDatabasePortException class.
        /// </summary>
        /// <param name="message"></param>
        public InvalidDatabasePortException(string message) : base(message)
        {
        }
    }
}