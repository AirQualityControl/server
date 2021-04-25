namespace AirSnitch.Core.Infrastructure.EventNotification
{
    /// <summary>
    /// Interface that represent abstract event notification receiver
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEventNotificationReceiver<in T> where T : BaseEventNotification
    {
        void Handle(T eventNotification);
    }
}