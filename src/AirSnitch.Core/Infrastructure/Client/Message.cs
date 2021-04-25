using AirSnitch.Core.Domain.Models;

namespace AirSnitch.Core.Infrastructure.Client
{
    /// <summary>
    /// Abstract class that represent message that will be send to User.
    /// </summary>
    public abstract class Message
    {
        /// <summary>
        /// Unique identifier of message recipient
        /// </summary>
        /// <value></value>
        public User Recipient { get; set; }
    }
}