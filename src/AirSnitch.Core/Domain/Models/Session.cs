using System;

namespace AirSnitch.Core.Domain.Models
{
    /// <summary>
    /// Class that define users manual interaction with client.(Telegram bot, Facebook Messenger, etc..)
    /// In case of bot it will be a message to a bot sent by a user
    /// </summary>
    public class Session
    {
        private bool _isEmpty;

        /// <summary>
        /// Unique identifier of client's unique id.
        /// </summary>
        public long ClientUserid { get; set; }
        
        /// <summary>
        /// Date and time when interaction occured
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// User message that send to client in current session
        /// </summary>
        public string UserMessage { get; set; }
    }
}