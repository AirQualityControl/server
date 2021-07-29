namespace AirSnitch.Domain.Models
{
    public class Email
    {
        public Email(string email)
        {
            Value = email;
        }
        
        /// <summary>
        /// Returns a string representation of user name.
        /// </summary>
        public string Value { get; }
    }
}