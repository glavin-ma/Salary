using System.Security.Claims;
using System.Threading.Tasks;
using DTO.Auth;

using Models.Employment;


namespace WebClient.Interfaces
{
    public interface IAuthService
    {
        Task<Employee> CheckCredentials(LoginDataDto request);
        Task<string> GenerateEncodedToken(LoginDataDto request, Employee user);
        Task<Employee> GetCurrentUser(ClaimsPrincipal currentUser);
    }
}
