using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Models.Auth;
using Models.Employment;

namespace Services.Classes
{
    public static class Extensions
    {
        public static double CalculateAllowance(this Employee employee, DateTime date)
        {
            var years = new DateTime((date - employee.EmploymentDate).Ticks).Year - 1;
            var percent = years * employee.Type.YearAllowance;
            return (percent > employee.Type.MaxAllowance ? employee.Type.MaxAllowance : percent) * employee.BasicRate / 100;
        }

        public static SigningCredentials GetSigningCredentials(this JwtOptions options)
        {
            return new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options.Secret)), SecurityAlgorithms.HmacSha256);
        }

        public static SymmetricSecurityKey GetSecurityKey(this JwtOptions options)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options.Secret));
        }
    }
}
