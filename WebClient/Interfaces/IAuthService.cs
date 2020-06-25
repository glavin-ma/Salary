using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Models.Auth;
using Newtonsoft.Json;

namespace WebClient.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateJwt(ClaimsIdentity identity, string userName, JsonSerializerSettings serializerSettings);
        Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}
