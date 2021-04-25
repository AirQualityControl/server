using System;

namespace AirSnitch.Core.Infrastructure.EventNotification.Attributes
{
    public class TargetEventNotificationAttribute : Attribute
    {
        private readonly Type _targetType;
        public TargetEventNotificationAttribute(Type eventNotificationType)
        {
            _targetType = eventNotificationType;
        }
    }
}