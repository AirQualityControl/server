using System;
using System.Collections.Generic;
using AirSnitch.Core.Domain.Models;
using AirSnitch.Core.Infrastructure.Jobs;
using AirSnitch.Core.UseCases.CancelAirMonitoring;

namespace AirSnitch.Core.UseCases.UserBlocksAppUseCase
{
    public class UserBlockedClientUseCaseParams : IUseCaseParam
    {
        public User User { get; set; }
        public List<IRecurringJob> JobsToCancel { get; set; }

        public void Validate()
        {
            if (User == null && JobsToCancel == null)
            {
                throw new ArgumentException("Invalid arguments");
            }
        }
    }
}