using System;
using System.Security.Claims;

namespace FinancialManager.Core
{
    internal static class ClaimsPrincipalExtensions
    {
        internal static string GetId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.ClaimValue(ClaimTypes.NameIdentifier);
        }

        internal static string GetMail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.ClaimValue(ClaimTypes.Email);
        }

        internal static string GetName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.ClaimValue(ClaimTypes.Name);
        }

        internal static string ClaimValue(this ClaimsPrincipal principal, string claimName)
        {
            var claim = principal.FindFirst(claimName);
            return claim?.Value;
        }
    }
}
