using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace FinancialManager.Identity
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal), "Claims principal is null.");

            return principal.ClaimValue(ClaimTypes.Email);
        }

        public static string GetName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal), "Claims principal is null.");

            return principal.ClaimValue(ClaimTypes.Name);
        }

        public static IEnumerable<string> GetRoles(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal), "Claims principal is null.");

            return principal.Claims.Where(p => p.Type == ClaimTypes.Role).Select(p => p.Value);
        }

        private static string ClaimValue(this ClaimsPrincipal principal, string claimName)
        {
            var claim = principal.FindFirst(claimName);
            return claim?.Value;
        }
    }
}