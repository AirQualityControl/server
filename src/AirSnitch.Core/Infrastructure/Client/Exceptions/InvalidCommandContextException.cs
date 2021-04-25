using System;
using System.Runtime.Serialization;

namespace AirSnitch.Core.Infrastructure.Client.Exceptions
{
    public class InvalidCommandContextException : Exception
    {
        public InvalidCommandContextException()
        {
        }

        protected InvalidCommandContextException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidCommandContextException(string message) : base(message)
        {
        }

        public InvalidCommandContextException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}