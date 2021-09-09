using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace FinancialManager.Infrastructure.Identity
{
    internal static class InitializerOptionsFactory
    {
        public static ref IdentityOptions SetIdentityOptions(ref IdentityOptions identityOptions)
        {
            identityOptions.Password = new()
            {
                RequireDigit = false,
                RequireLowercase = false,
                RequireNonAlphanumeric = false,
                RequireUppercase = false,
                RequiredLength = 6,
                RequiredUniqueChars = 1
            };
            identityOptions.Lockout = new()
            {
                DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5),
                MaxFailedAccessAttempts = 5,
                AllowedForNewUsers = true
            };
            identityOptions.User = new()
            {
                AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@",
                RequireUniqueEmail = false
            };

            return ref identityOptions;
        }

        public static ref JwtBearerOptions SetJwtOptions(ref JwtBearerOptions jwtOptions,
            JwtSettings settings)
        {
            jwtOptions.TokenValidationParameters = BuildTokenConfiguration(settings.SecretKey, settings.Audience, settings.Issuer);
            
            return ref jwtOptions;
        }

        public static ref AuthenticationOptions SetAuthenticationOptions(ref AuthenticationOptions authOptions)
        {
            authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            return ref authOptions;
        }

        public static TokenValidationParameters BuildTokenConfiguration(string secretKey,
            string audience, string issuer) =>
         new()
         {
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
             ValidAudience = audience,
             ValidIssuer = issuer,
             ValidateIssuerSigningKey = true,
             ValidateAudience = true,
             ValidateLifetime = true,
         };

        public static AuthorizationPolicy BuildRequiredAuthenticatePolicy() =>
            new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
    }
}
