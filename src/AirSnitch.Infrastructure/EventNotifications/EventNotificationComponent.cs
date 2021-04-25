using System;
using AirSnitch.Core.Infrastructure;

namespace AirSnitch.Infrastructure.EventNotifications
{
    public class EventNotificationComponent : ISystemComponent
    {
        public void CheckComponent()
        {
            EventNotificationReceiverFactory.RegisterReceiversFromCurrentAssembly();
            Console.WriteLine("Event notification system successfully established");
        }
    }
}