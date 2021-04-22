using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.Authorization
{
    public class AdminRequirement : IAuthorizationRequirement
    {
    }
}
