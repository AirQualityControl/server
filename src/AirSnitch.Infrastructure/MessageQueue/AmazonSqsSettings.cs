using System;
using System.Collections.Generic;

namespace AirSnitch.Infrastructure.MessageQueue
{
    /// <summary>
    /// Class the represent a configurable Amazon SQS Settings
    /// </summary>
    public class AmazonSqsSettings
    {
        private string _queueUrl;
        public string QueueUrl {
            get
            {
                return _queueUrl;
            }
            set
            {
                _queueUrl = value;
                Console.WriteLine($"This is a value {value}");
            }
        }

        public string ServiceUrl { get; set; }

        public string AccessKey { get; set; }

        private string _accessSecrete;
        public string AccessSecrete {
            get
            {
                return _accessSecrete;
            }
            set
            {
                _accessSecrete = value;
                Console.WriteLine($"This is a secrete {_accessSecrete}");
                
            }
        }
        
        public int RequestTimeOut { get; set; }
        
        public int MaxNumberOfMessagesInRequest { get; set; }
    }
}