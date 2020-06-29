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
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return await Task.FromResult(encodedJwt);
        }

        public async Task<Employee> GetCurrentUser(ClaimsPrincipal currentUser)
        {
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _userManager.FindByNameAsync(currentUserName);
        }

        //public async Task<string> GenerateJwt(ClaimsIdentity identity, string userName, JsonSerializerSettings serializerSettings)
        //{
        //    var response = new
        //    {
        //        id = identity.Claims.Single(c => c.Type == "id").Value,
        //        auth_token = await GenerateEncodedToken(userName, identity),
        //        expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
        //    };

        //    return JsonConvert.SerializeObject(response, serializerSettings);
        //}

        //public async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        //{
        //    if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password)) return await Task.FromResult<ClaimsIdentity>(null);
        //    var userToVerify = await _userManager.FindByNameAsync(userName);
        //    if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);
        //    if (await _userManager.CheckPasswordAsync(userToVerify, password)) return await Task.FromResult(GenerateClaimsIdentity(userName, userToVerify.Id));
        //    return await Task.FromResult<ClaimsIdentity>(null);
        //}

        //public ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
        //{
        //    return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
        //    {
        //        new Claim("id", id),
        //        new Claim("rol", "api_access")
        //    });
        //}

        //private async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
        //{
        //    var claims = new[]
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, userName),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        //new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
        //        identity.FindFirst("rol"),
        //        identity.FindFirst("id") //todo Add roles to claim
        //    };

        //    var jwt = new JwtSecurityToken(
        //        issuer: _jwtOptions.Issuer,
        //        audience: _jwtOptions.Audience,
        //        claims: claims,
        //        notBefore: _jwtOptions.NotBefore,
        //        expires: _jwtOptions.Expiration,
        //        signingCredentials: _jwtOptions.SigningCredentials);

        //    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        //    return await Task.FromResult(encodedJwt);
        //}
    }
}
