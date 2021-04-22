using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.Authorization
{
    public static class Policies
    {
        public const string RequiredUser = nameof(RequiredUser);
        public const string RequiredAdmin = nameof(RequiredAdmin);
    }
}
