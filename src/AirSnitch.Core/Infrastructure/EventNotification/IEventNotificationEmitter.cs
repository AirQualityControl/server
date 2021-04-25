namespace AirSnitch.Core.Infrastructure.EventNotification
{
    /// <summary>
    /// Abstract interface that declare functionality for every concrete message emitter
    /// </summary>
    public interface IEventNotificationEmitter<T> where T : BaseEventNotification
    {
        /// <summary>
        /// Returns an instance of eventNotificationEmitter
        /// </summary>
        static IEventNotificationEmitter<T> Instance { get; }
        
        /// <summary>
        /// Emmit new notification.
        /// Method executes synchronously and never throw and exception.
        /// </summary>
        /// <param name="notification">BaseEventNotification eventNotification</param>
        void EmmitEventNotification(T notification);
    }
}