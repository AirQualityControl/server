using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using AirSnitch.Infrastructure.Abstract.MessageQueue;

namespace AirSnitch.Worker.AirPollutionConsumer.Pipeline
{
    public class AcknowledgeMessageBlock
    {
        private readonly IDistributedMessageQueue _distributedMessageQueue;

        public AcknowledgeMessageBlock(IDistributedMessageQueue distributedMessageQueue)
        {
            _distributedMessageQueue = distributedMessageQueue;
        }
        public ActionBlock<Message> Instance => new ActionBlock<Message>(Action);
        
        private async Task Action(Message message)
        {
            await _distributedMessageQueue.DeleteMessageAsync(message);
        }
    }
}