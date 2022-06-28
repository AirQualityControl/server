using System.Threading.Tasks.Dataflow;
using AirSnitch.Infrastructure.Abstract.MessageQueue;

namespace AirSnitch.Worker.AirPollutionConsumer.Pipeline
{
    public class AcknowledgeMessageBlock
    {
        public ActionBlock<Message> Instance = new ActionBlock<Message>(async uri =>
        {
           
        });
    }
}