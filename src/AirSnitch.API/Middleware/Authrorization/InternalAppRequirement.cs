using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AirSnitch.Api.Middleware.Authrorization
{
    public class InternalAppRequirement : AuthorizationHandler<InternalAppRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, InternalAppRequirement requirement)
        {
            var currentAppPrincipal = context.User;
            
            if (InternalAppIdentity.VerifyPrincipal(currentAppPrincipal))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            context.Fail();
            return Task.CompletedTask;
        }
    }
}