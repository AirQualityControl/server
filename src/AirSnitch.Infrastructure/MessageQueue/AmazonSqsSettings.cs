namespace AirSnitch.Infrastructure.MessageQueue
{
    /// <summary>
    /// Class the represent a configurable Amazon SQS Settings
    /// </summary>
    public class AmazonSqsSettings
    {
        public string QueueUrl { get; set; }

        public string ServiceUrl { get; set; }

        public string AccessKey { get; set; }
        public string AccessSecrete { get; set; }

        public int RequestTimeOut { get; set; }
        
        public int MaxNumberOfMessagesInRequest { get; set; }
    }
}