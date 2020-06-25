using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DAL.Context;
using DTO.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Auth;
using Models.Employment;
using Newtonsoft.Json;
using Services.Classes;
using WebClient.Interfaces;

namespace WebClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private readonly IAuthService _authService;

        public AuthController( IAuthService authService )
        {

            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] LoginDataDto request)
        {
            var identity = await _authService.GetClaimsIdentity(request.UserName, request.Password);
            if (identity == null) throw new HandledException("Invalid username or password.");
            var jwt = await _authService.GenerateJwt(identity, request.UserName, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        
        //[HttpGet]
        //public IActionResult Login([FromQuery] LoginDataDto request)
        //{
        //    throw new HandledException("Message test");
        //}
    }
}
