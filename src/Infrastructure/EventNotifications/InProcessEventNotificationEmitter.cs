using System;
using AirSnitch.Core.Infrastructure.EventNotification;

namespace AirSnitch.Infrastructure.EventNotifications
{
    /// <summary>
    /// Implementation of interface IEventNotificationEmitter that provides in-process massaging facilities.
    /// </summary>
    public class InProcessEventNotificationEmitter<T> : IEventNotificationEmitter<T> where T : BaseEventNotification
    {
        public InProcessEventNotificationEmitter()
        {
            
        }
        
        private readonly IEvenNotificationStore<T> _evenNotificationStore;
        private InProcessEventNotificationEmitter(IEvenNotificationStore<T> store)
        {
            _evenNotificationStore = store;
        }
        
        private static readonly Lazy<InProcessEventNotificationEmitter<T>> 
            Emitter = new Lazy<InProcessEventNotificationEmitter<T>>(
                () => new InProcessEventNotificationEmitter<T>(
                    new InMemoryEventNotificationStore<T>()));
        
        public static IEventNotificationEmitter<T> Instance => Emitter.Value;

        ///<inheritdoc/>
        public void EmmitEventNotification(T eventNotification)
        {
            _evenNotificationStore.AddEvent(eventNotification);
        }
    }
}