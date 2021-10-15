using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AirSnitch.Api.Middleware.Authrorization
{
    public class InteralAppRequirement : AuthorizationHandler<InteralAppRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, InteralAppRequirement requirement)
        {
            var currentUserIdentity = context.User;

            if (InternalAppIdentity.IsEqualsTo(currentUserIdentity))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            context.Fail();
            return Task.CompletedTask;
        }
    }
}