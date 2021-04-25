using AirSnitch.Core.Infrastructure.EventNotification;
using AirSnitch.Core.Infrastructure.Jobs.Background;
using AirSnitch.Core.Infrastructure.JobStore;
using Serilog;

namespace AirSnitch.Infrastructure.EventNotifications
{
    public class EventNotificationConsumer<T> where T : BaseEventNotification
    {
        private readonly IJobExecutor _backgroundJobExecutor;

        public EventNotificationConsumer()
        {
            _backgroundJobExecutor = new BackgroundJobExecutor();
        }
        
        public void ConsumeItem(T eventNotification)
        {
            Log.Information($"New event was received!");
            
            bool isInstanceCreated = EventNotificationReceiverFactory.TryGetEventNotificationReceiver<T>(out var eventNotificationReceiver);
            
            if (isInstanceCreated)
            {
                var receiverBackgroundJob = new BaseNotificationReceiverBackgroundJob()
                {
                    MethodToCall = () => eventNotificationReceiver.Handle(eventNotification)
                };
                _backgroundJobExecutor.ExecuteJob(receiverBackgroundJob);
            }
        }
    }
}