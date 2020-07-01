using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DTO.Employment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using WebClient.Interfaces;

namespace WebClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService EmpService { get; set; }
        private IMapper Mapper { get; set; }
        private IAuthService AuthService { get; set; }
        public EmployeeController(IEmployeeService empService, IMapper mapper, IAuthService authService)
        {
            EmpService = empService;
            Mapper = mapper;
            AuthService = authService;
        }

        [Authorize(Roles = "Manager,Salesman")]
        [HttpGet("calculate")]
        public IEnumerable<EmployeeDto> Get(DateTime date)
        {
            var data = EmpService.CalculateSalary(date);

            return Mapper.Map<IEnumerable<EmployeeDto>>(data).OrderBy(p => p.FullName);
        }

        [Authorize]
        [HttpGet]
        public EmployeeInfoDto Get()
        {
            var user = AuthService.GetCurrentUser(this.User).Result;
            var emp = EmpService.GetEmployeeWithCalculation(user.Id);
            return Mapper.Map<EmployeeInfoDto>(emp);
        }
    }
}
