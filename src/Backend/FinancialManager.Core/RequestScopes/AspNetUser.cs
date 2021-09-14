using Microsoft.AspNetCore.Http;
using System;

namespace FinancialManager.Core
{
    public interface IAspNetUser
    {
        string Name { get; }
        Guid Id { get; }
        string Mail { get; }
    }

    public class AspNetUser : IAspNetUser
    {
        private readonly HttpContext _httpContext;

        public AspNetUser(IHttpContextAccessor acessor) =>
            _httpContext = acessor.HttpContext;

        private bool Authenticated => _httpContext?.User?.Identity?.IsAuthenticated ?? false;

        public string Name => Authenticated ? _httpContext.User.GetName() : string.Empty;

        public Guid Id
        {
            get
            {
                if (!Authenticated || !Guid.TryParse(_httpContext.User.GetId(), out var userId))
                    return Guid.Empty;
                return userId;
            }
        }

        public string Mail => Authenticated ? _httpContext.User.GetMail() : string.Empty;
    }
}
