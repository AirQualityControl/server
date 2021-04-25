

using System;
using System.Diagnostics.Contracts;

namespace AirSnitch.Core.Domain.Models
{
    /// <summary>
    /// Abstract class that describes properties that is common for all types of clients
    /// </summary>
    public class ClientInfo
    {
        /// <summary>
        /// User friendly client name
        /// For instance Telegram. Facebook messenger, etc...
        /// </summary>
        private string _name;
        
        public string Name
        {
            get => _name;
            set
            {
                Contract.Requires(!String.IsNullOrEmpty(value));
                _name = value;
            }
        }

        
        /// <summary>
        /// Property determine whether client was banned b y the user.
        /// In case of chat-bots when user delete and stop the bot 
        /// </summary>
        public bool IsBanned { get; set; }
    }
}