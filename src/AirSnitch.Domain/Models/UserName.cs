namespace AirSnitch.Domain.Models
{
    /// <summary>
    /// ADT that represent a user name
    /// </summary>
    public class UserName
    {
        public UserName(string name)
        {
            Value = name;
        }
        
        /// <summary>
        /// Returns a string representation of user name.
        /// </summary>
        public string Value { get; }
    }
}