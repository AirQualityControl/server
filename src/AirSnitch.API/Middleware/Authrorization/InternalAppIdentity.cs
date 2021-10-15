using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using DeclarativeContracts.Exceptions;
using DeclarativeContracts.Precondition;

namespace AirSnitch.Api.Middleware.Authrorization
{
    internal class InternalAppIdentity
    {
        private static readonly Guid DevPanelClientId = new Guid("16bc361c-30aa-4f4a-9056-6f3d1ef34f0e");
        
        private static List<Guid> _internalApps = new List<Guid>()
        {
            DevPanelClientId
        };
        
        public static bool IsEqualsTo(ClaimsPrincipal userPrincipal)
        {
            var idClaim = GetIdClaim(userPrincipal.Claims);

            return _internalApps.FirstOrDefault(id =>  id.Equals(idClaim)) != Guid.Empty;
        }

        private static Guid GetIdClaim(IEnumerable<Claim> userPrincipalClaims)
        {
            var idClaims = userPrincipalClaims.Where(c => c.Type == Constants.Claim.ClientId).ToList();

            if (!idClaims.Any())
            {
                throw new ContractViolationException($"{Constants.Claim.ClientId} was not found in current user claims");
            }

            if (idClaims.Count() > 1)
            {
                throw new ConstraintException($"There are more that one {Constants.Claim.ClientId} claims was found!");
            }

            return new Guid(idClaims.Single().Value);
        }
    }
}