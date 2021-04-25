using System;
using System.Linq.Expressions;
using AirSnitch.Core.Infrastructure.JobStore;

namespace AirSnitch.Core.Infrastructure.EventNotification
{
    /// <summary>
    /// Class that represent a background job for
    /// every event notification receiver in the system
    /// </summary>
    public class BaseNotificationReceiverBackgroundJob : IBackgroundJob
    {
        public Expression<Action> MethodToCall { get; set; }
        public bool IsValid()
        {
            return MethodToCall != null;
        }
    }
}