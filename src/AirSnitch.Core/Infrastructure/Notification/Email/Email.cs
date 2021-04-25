namespace AirSnitch.Core.Infrastructure.Notification.Email
{
    /// <summary>
    /// Entity that represent abstract email that will be sent from system
    /// </summary>
    public abstract class Email
    {
        /// <summary>
        /// Email of target reciver
        /// </summary>
        public string ReceiverEmail { get; set; }

        /// <summary>
        /// Email subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Email body
        /// </summary>
        public string Body { get; set; }
    }
}