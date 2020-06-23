using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Employment;

namespace WebClient.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IDataContext _dataContext;
        public AuthController(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpPost]
        public Task<IActionResult> Login(string username, string password)
        {
            throw new ArgumentException("Message test");
        }
        [HttpGet]
        public IActionResult Login()
        {
            throw new ArgumentException("Message test");
        }
    }
}
