using System;

namespace AirSnitch.Infrastructure.MessageQueue
{
    /// <summary>
    /// Class the represent a configurable Amazon SQS Settings
    /// </summary>
    public class AmazonSqsSettings
    {
        private string _queueUrl = "";
        public string QueueUrl {
            get => _queueUrl;
            set
            {
                _queueUrl = value;
                Console.WriteLine($"Queue url is {_queueUrl}");
            }
        }

        private string _serviceUrl = "";

        public string ServiceUrl
        {
            get => _serviceUrl;
            set
            {
                _serviceUrl = value;
                Console.WriteLine($"Service url is {_serviceUrl}");
            }
        }

        public string AccessKey { get; set; }
        public string AccessSecrete { get; set; }

        public int RequestTimeOut { get; set; }
        
        public int MaxNumberOfMessagesInRequest { get; set; }
    }
}
