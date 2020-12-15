namespace FinancialManager.Identity.Jwt
{
	public record AppJwtSettings
	{
		public const string CONFIG_NAME = "AppJwtSettings";

		public string SecretKey { get; set; }
		public int Expiration { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }
	}
}
