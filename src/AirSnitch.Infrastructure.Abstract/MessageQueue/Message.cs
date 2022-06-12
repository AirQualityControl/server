using System.Collections.Generic;

namespace AirSnitch.Infrastructure.Abstract.MessageQueue
{
    public class Message
    {
        /// <summary>
        /// Message Id
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Message body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Hash value of the message body
        /// </summary>
        public string BodyMD5Hash { get; set; }

        /// <summary>
        /// Message attributes.
        /// This dictionary could hold attributes and specific message details.
        /// </summary>
        public Dictionary<string, string> Attributes { get; set; }
    }
}