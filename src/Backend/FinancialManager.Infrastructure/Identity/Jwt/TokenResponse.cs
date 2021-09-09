namespace FinancialManager.Infrastructure.Identity
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
    }
}
