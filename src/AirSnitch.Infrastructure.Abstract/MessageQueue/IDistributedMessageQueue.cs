using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;

namespace AirSnitch.Infrastructure.Abstract.MessageQueue
{
    public interface IDistributedMessageQueue
    {
        /// <summary>
        ///     Returns an approximate number of messages in queue
        /// </summary>
        /// <returns>Returns a value that represent a number of messages</returns>
        Task<BigInteger> GetApproximateMessageCount();

        /// <summary>
        ///     Returns a batch of messages from queue
        /// </summary>
        /// <returns></returns>
        Task<List<Message>> GetMessageBatchAsync(int batchSize);

        /// <summary>
        ///     Delete a batch of messages asynchronously
        /// </summary>
        /// <returns></returns>
        Task DeleteMessageBatchAsync(List<Message> mmsBatchToDelete);

        /// <summary>
        ///     Delete message from queue asynchronously
        /// </summary>
        /// <param name="msg">Target message</param>
        /// <returns>Task</returns>
        Task DeleteMessageAsync(Message msg);
    }
}