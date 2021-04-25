using System;
using System.Collections.Generic;
using AirSnitch.Core.Infrastructure.EventNotification;

namespace AirSnitch.Infrastructure.EventNotifications
{
    /// <summary>
    /// Factory that responsible for registration and resolving event notifications and their receivers
    /// </summary>
    internal static class EventNotificationReceiverFactory
    {
        private static readonly Dictionary<Type, Type> EventNotificationReceiversMap = new Dictionary<Type, Type>();
        
        /// <summary>
        /// Automagically find and map each eventNotification to corresponding eventNotificationReceivers.
        /// </summary>
        public static void RegisterReceiversFromCurrentAssembly()
        {
            //EventNotificationReceiversMap.Add(typeof(NewUserAddedEventNotification), typeof(NewUserAddedEventNotificationReceiver));
        }

        /// <summary>
        /// Try get an instance of event notification. In case of success set reference to already created object in eventNotificationReceiver
        /// </summary>
        /// <param name="eventNotificationReceiver">Reference that will point out to successfully create instance</param>
        /// <typeparam name="T">Type of event notification</typeparam>
        /// <returns>True in case of success, false - otherwise</returns>
        public static bool TryGetEventNotificationReceiver<T>(out IEventNotificationReceiver<T> eventNotificationReceiver) 
            where T : BaseEventNotification
        {
            var eventNotificationType = typeof(T);
            
            if (EventNotificationReceiversMap.TryGetValue(eventNotificationType, out var eventNotificationReceiverType))
            {
                IEventNotificationReceiver<T> createdInstance = CreateReceiverInstance<T>(eventNotificationReceiverType);
                if (createdInstance != null)
                {
                    eventNotificationReceiver = createdInstance;
                    return true;
                }
            }
            eventNotificationReceiver = null;
            return false;
        }

        private static IEventNotificationReceiver<T> CreateReceiverInstance<T>(Type targetType) where T : BaseEventNotification
        {
            IEventNotificationReceiver<T> targetTypeInstance = null;
            try
            {
                targetTypeInstance = (IEventNotificationReceiver<T>)Activator.CreateInstance(targetType);
            }
            catch (Exception ex)
            {
                //TODO: Inject logger and log 
                //Logger.Error($"Exception occurred during activation of type {targetType}." +
                              //$"Check for exception details for more information");
            }
            return targetTypeInstance;
        }
    }
}