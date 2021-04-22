using AirSnitch.Api.Models.Internal;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.Authorization
{
    public class UserAuthorizationHandler : AuthorizationHandler<UserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRequirement requirement)
        {
            if (context.User.IsInRole(Roles.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
