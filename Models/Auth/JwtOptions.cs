using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
        public TimeSpan ValidFor => TimeSpan.FromMinutes(LifeTime);
        public SigningCredentials SigningCredentials => new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret)), SecurityAlgorithms.HmacSha256);


        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
        //public DateTime NotBefore => DateTime.UtcNow;
        //public DateTime IssuedAt => DateTime.UtcNow;
    
}
