namespace CF.Identity.API.Settings
{
    public class JwtAuthenticationSettings
    {
        public string SecretKey { get; set; }
        public int ExpirationTimeInHours { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

        public JwtAuthenticationSettings()
        {

        }

        public JwtAuthenticationSettings(string secretKey,
                                         int expirationTimeInHours,
                                         string issuer,
                                         string audience)
        {
            SecretKey = secretKey;
            ExpirationTimeInHours = expirationTimeInHours;
            Issuer = issuer;
            Audience = audience;
        }
    }
}
