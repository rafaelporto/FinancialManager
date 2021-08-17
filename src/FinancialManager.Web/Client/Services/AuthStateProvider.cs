using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;

namespace FinancialManager.Client.Services
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private const string ACCESS_TOKEN = "access_token";
        private const string JWT = "jwt";
        private const string BEARER = "Bearer";

        public AuthStateProvider(ILocalStorageService localStorageService) =>
            _localStorageService = localStorageService;

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await _localStorageService.GetItemAsync<string>(ACCESS_TOKEN);

            if (string.IsNullOrWhiteSpace(savedToken))
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            try
            {
                var claimsIdentity = new ClaimsIdentity(ParseClaimsFromJwt(savedToken), JWT);
                return new AuthenticationState(new ClaimsPrincipal(claimsIdentity));
            }
            catch (Exception)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
            await _localStorageService.SetItemAsync(ACCESS_TOKEN, token);

            var claims = ParseClaimsFromJwt(token);
            var givenNameClaim = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.GivenName);

            var identity = new ClaimsIdentity(new[]
            {
                givenNameClaim,
                claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email),
                new Claim(ClaimsIdentity.DefaultNameClaimType, givenNameClaim.Value)
            }, BEARER);

            foreach (var role in claims.Where(p => p.Type == ClaimTypes.Role))
                identity.AddClaim(role);

            var authenticatedUser = new ClaimsPrincipal(identity);

            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public async Task MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            await _localStorageService.ClearAsync();
            NotifyAuthenticationStateChanged(authState);
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwtToken)
        {
            var payload = jwtToken.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            return keyValuePairs.Select(p => new Claim(p.Key, p.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
