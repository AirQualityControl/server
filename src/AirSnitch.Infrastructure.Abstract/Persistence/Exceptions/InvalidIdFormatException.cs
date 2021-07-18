using System;

namespace AirSnitch.Infrastructure.Abstract.Persistence.Exceptions
{
    public class InvalidIdFormatException : Exception
    {
        public InvalidIdFormatException()
        {
            
        }

        public InvalidIdFormatException(string message): base(message)
        {
            
        }
    }
}