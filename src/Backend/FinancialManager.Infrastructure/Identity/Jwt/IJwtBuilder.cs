using Microsoft.AspNetCore.Identity;

namespace FinancialManager.Infrastructure.Identity
{
    internal interface IJwtBuilder
    {
        IManagerJwtBuilder WithUserManager(UserManager<ApplicationUser> userManager);
    }

    internal interface IManagerJwtBuilder
    {
        IOptionsJwtBuilder WithJwtSettings(JwtSettings appJwtSettings);
    }

    internal interface IOptionsJwtBuilder
    {
        IEmailJwtBuilder WithEmail(string email);
    }

    internal interface IEmailJwtBuilder 
    {
        IEmailJwtBuilder WithJwtClaims();
        IEmailJwtBuilder WithUserClaims();
        IEmailJwtBuilder WithUserRoles();

        ITokenJwtBuilder BuildToken();
    }

    internal interface ITokenJwtBuilder 
    {
        string GetToken();

        TokenResponse GetTokenResponse();
    }
}
