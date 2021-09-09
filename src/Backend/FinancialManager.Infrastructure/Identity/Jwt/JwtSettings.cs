namespace FinancialManager.Infrastructure.Identity
{
    internal class JwtSettings
    {
        public const string CONFIG_NAME = "IdentitySettings";

        public string SecretKey { get; set; }
        public int Expiration { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
