using System.Threading.Channels;
using System.Threading.Tasks;
using AirSnitch.Core.Infrastructure.EventNotification;

namespace AirSnitch.Infrastructure.EventNotifications
{
    /// <summary>
    /// Implementation of interface eventNotificationStore that store event in memory queue
    /// </summary>
    public class InMemoryEventNotificationStore<T> : IEvenNotificationStore<T> where T : BaseEventNotification
    {
        private static Channel<T> _channel;

        public InMemoryEventNotificationStore()
        {
            CreateChanelInstance();
            WaitForNewItem();
        }

        /// <summary>
        /// Add new event to queue
        /// </summary>
        /// <param name="eventNotification"></param>
        public void AddEvent(T eventNotification)
        {
            _channel.Writer.TryWrite(eventNotification);
        }

        private static void CreateChanelInstance()
        {
            _channel = Channel.CreateUnbounded<T>();
        }
        
        private void WaitForNewItem()
        {
            Task.Run(async () =>
            {
                while (await _channel.Reader.WaitToReadAsync())
                {
                    var eventNotification = await _channel.Reader.ReadAsync();
                    
                    var eventNotificationConsumer = new EventNotificationConsumer<T>();
                    eventNotificationConsumer.ConsumeItem(eventNotification);
                }
            });
        }
    }
}