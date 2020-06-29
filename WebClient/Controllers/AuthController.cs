using System.Threading.Tasks;
using DTO.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Employment;
using Services.Classes;
using WebClient.Interfaces;

namespace WebClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] LoginDataDto request)
        {
            var user = await _authService.CheckCredentials(request);
            if (user == null) throw new HandledException("Invalid username or password.");
            var jwt = await _authService.GenerateEncodedToken(request, user);
            return new OkObjectResult(new { accessToken = jwt });
        }

        [Authorize]
        [HttpGet("currentUser")]
        public async Task<Employee> Get()
        {
            return await _authService.GetCurrentUser(this.User);
        }
    }
}
