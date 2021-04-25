namespace AirSnitch.Core.Infrastructure.EventNotification
{
    /// <summary>
    /// Interface that represent store for all emitted events
    /// </summary>
    public interface IEvenNotificationStore<in T> where T : BaseEventNotification
    {
        /// <summary>
        /// Add event to event notification store
        /// </summary>
        /// <param name="eventNotification"></param>
        void AddEvent(T eventNotification);
    }
}