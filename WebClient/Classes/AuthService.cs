using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using DTO.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Models.Auth;
using Models.Employment;
using Newtonsoft.Json;
using Services.Classes;
using WebClient.Interfaces;

namespace WebClient.Classes
{
    public class AuthService : IAuthService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthService(IOptions<JwtOptions> jwtOptions, UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager)
        {
            _jwtOptions = jwtOptions.Value;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Employee> CheckCredentials(LoginDataDto request)
        {
            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password)) return null;
            var userToVerify = await _userManager.FindByNameAsync(request.UserName);
            if (userToVerify == null) return null;
            var superuser = false;
#if DEBUG
            superuser = userToVerify.UserName.ToLower() == "superuser";
#endif
            return await _userManager.CheckPasswordAsync(userToVerify, request.Password.ToUpper()) || superuser ? userToVerify : null;
        }

        public async Task<string> GenerateEncodedToken(LoginDataDto request, Employee user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, request.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.Id)
            };
            foreach (var role in await _userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim("role", role));
            }
            claims.Add(new Claim("role", "test"));
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.GetSigningCredentials());

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return await Task.FromResult(encodedJwt);
        }

        public async Task<Employee> GetCurrentUser(ClaimsPrincipal currentUser)
        {
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _userManager.FindByNameAsync(currentUserName);
        }
    }
}
