using AirSnitch.Core.Domain.Models;
using AirSnitch.Core.Infrastructure.Client.Exceptions;

namespace AirSnitch.Core.Infrastructure.Client
{
    /// <summary>
    /// Contextual information for command 
    /// </summary>
    public class CommandContext
    {
        /// <summary>
        /// User who initialize current command
        /// </summary>
        /// <value></value>
        public User User { get; set; }

        /// <summary>
        /// Unique identifier of chat between user and client
        /// </summary>
        /// <value></value>
        public long ChatId { get; set; }

        /// <summary>
        /// Verify whether current command context is valid for using in command
        /// </summary>
        /// <exception cref="InvalidCommandContextException"></exception>
        public virtual void Verify()
        {
            if (ChatId == default)
            {
                throw new InvalidCommandContextException($"Command context is invalid and not suitable for using in command.Parameter ChatId cannot be {ChatId}");
            }
        }
    }
}
