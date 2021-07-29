namespace AirSnitch.Domain.Models
{
    /// <summary>
    /// ADT that represent a LastName
    /// </summary>
    public class LastName
    {
        public LastName(string name)
        {
            Value = name;
        }
        
        /// <summary>
        /// Returns a string representation of user name.
        /// </summary>
        public string Value { get; }
    }
}