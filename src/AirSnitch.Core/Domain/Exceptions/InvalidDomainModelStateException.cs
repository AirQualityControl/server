using System;

namespace AirSnitch.Core.Domain.Exceptions
{
    public class InvalidDomainModelStateException : Exception
    {
        public InvalidDomainModelStateException()
        {
            
        }

        public InvalidDomainModelStateException(string exceptionText): base(exceptionText)
        {
            
        }
        
        public InvalidDomainModelStateException(Exception ex, string exceptionText)
        {
            
        }
    }
}