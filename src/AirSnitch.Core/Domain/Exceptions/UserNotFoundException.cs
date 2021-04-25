using System;

namespace AirSnitch.Core.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        private new const string Message = "User was not found in DB. Plese check the client unique identifier!";
        
        public UserNotFoundException(string message = Message) : base(message)
        {
            
        }

        public UserNotFoundException(string message, Exception ex) : base(message, ex)
        {
            
        }
    }
}