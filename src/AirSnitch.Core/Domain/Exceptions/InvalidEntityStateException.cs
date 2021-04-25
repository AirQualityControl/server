using System;
using System.Runtime.Serialization;

namespace AirSnitch.Core.Domain.Exceptions
{
    /// <summary>
    /// Exception that identifies that domain entity is not in consistent state.
    /// For instance, mandatory fields are not filed in or contains invalid values.
    /// </summary>
    public class InvalidEntityStateException : Exception
    {
        public InvalidEntityStateException()
        {
        }

        protected InvalidEntityStateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidEntityStateException(string message) : base(message)
        {
        }

        public InvalidEntityStateException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}