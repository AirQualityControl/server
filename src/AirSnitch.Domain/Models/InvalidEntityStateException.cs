using System;
using System.Runtime.Serialization;

namespace AirSnitch.Domain.Models
{
    public class InvalidEntityStateException : Exception
    {
        public InvalidEntityStateException()
        {
        }

        protected InvalidEntityStateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidEntityStateException(string? message) : base(message)
        {
        }

        public InvalidEntityStateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}