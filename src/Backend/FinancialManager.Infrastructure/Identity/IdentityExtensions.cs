using FinancialManager.Core;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FinancialManager.Infrastructure.Identity
{
    public static class IdentityExtensions
    {
        public static IReadOnlyList<Notification> MapToDomainNotification(this IEnumerable<IdentityError> errors)
        {
            List<Notification> result = new();

            foreach (var error in errors)
                result.Add(new Notification(error.Code, error.Description));

            return result.AsReadOnly();
        }
    }
}
