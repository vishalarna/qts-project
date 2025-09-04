namespace QTD2.Infrastructure.JWT
{
    public class JWTSettings
    {
        public string TokenSecretKey { get; set; }

        public string ValidIssuer { get; set; }

        public string ValidAudience { get; set; }
    }
}
