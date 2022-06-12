using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using AirSnitch.Infrastructure.Abstract.MessageQueue;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Message = AirSnitch.Infrastructure.Abstract.MessageQueue.Message;

namespace AirSnitch.Infrastructure.MessageQueue
{
    public class AmazonSqsQueue : IDistributedMessageQueue
    {
        private readonly AmazonSQSClient _sqsClient;
        private readonly AmazonSqsSettings _settings;
        
        public AmazonSqsQueue(AmazonSqsSettings settings)
        {
            _settings = settings;
            var awsCredentials = new BasicAWSCredentials(_settings.AccessKey, _settings.AccessSecrete);
            var amazonSqsConfig = new AmazonSQSConfig
            {
                ServiceURL = _settings.ServiceUrl
            };
            _sqsClient = new AmazonSQSClient(awsCredentials, amazonSqsConfig);
        }
        
        public async Task<BigInteger> GetApproximateMessageCount()
        {
            var getQueueAttributesRequest = new GetQueueAttributesRequest()
            {
                QueueUrl = _settings.QueueUrl,
                AttributeNames = new List<string>(1){"ApproximateNumberOfMessages"}
            };
            
            var approximateMessageCount = await _sqsClient.GetQueueAttributesAsync(getQueueAttributesRequest);
            return approximateMessageCount.ApproximateNumberOfMessages;
        }

        public async Task<List<Message>> GetMessageBatchAsync(int batchSize)
        {
            var receiveMessageRequest = new ReceiveMessageRequest()
            {
                QueueUrl = _settings.QueueUrl,
                MaxNumberOfMessages = batchSize
            };
            
            var receiveMessageResponse = await _sqsClient.ReceiveMessageAsync(receiveMessageRequest);
            return receiveMessageResponse.Messages.Select(sqsMessage => new Message()
            {
                Body = sqsMessage.Body,
                Id = sqsMessage.MessageId,
                BodyMD5Hash = sqsMessage.MD5OfBody,
                Attributes = new Dictionary<string, string>()
                {
                    {"RECIPE_HANDLER", sqsMessage.ReceiptHandle}   
                }
            }).ToList();
        }

        public async Task DeleteMessageBatchAsync(List<Message> msgBatchToDelete)
        {
            var batchDeleteMessageRequest = new DeleteMessageBatchRequest()
            {
                QueueUrl = _settings.QueueUrl,
                Entries = msgBatchToDelete.Select(
                    m => new DeleteMessageBatchRequestEntry(
                        m.Id, m.Attributes["RECIPE_HANDLER"])
                    ).ToList()
            };

            await _sqsClient.DeleteMessageBatchAsync(batchDeleteMessageRequest);
        }

        public async Task DeleteMessageAsync(Message msg)
        {
            var deleteMessageRequest = new DeleteMessageRequest()
            {
                ReceiptHandle = msg.Attributes["RECIPE_HANDLER"]
            };

            await _sqsClient.DeleteMessageAsync(deleteMessageRequest);
        }
    }
}