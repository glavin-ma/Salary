using System;

namespace Models.Auth
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public string Audience { get; set; }
        public double LifeTime { get; set; }
        public DateTime NotBefore => DateTime.UtcNow;
        public DateTime Expiration => DateTime.UtcNow.AddMinutes(LifeTime);
    }
}
