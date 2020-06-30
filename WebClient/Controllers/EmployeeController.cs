using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DTO.Employment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WebClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService EmpService { get; set; }
        private IMapper Mapper { get; set; }
        public EmployeeController(IEmployeeService empService, IMapper mapper)
        {
            EmpService = empService;
            Mapper = mapper;
        }

        [Authorize]
        public async Task<IEnumerable<EmployeeDto>> Get()
        {
            var data= await EmpService.GetEmployees();
            return Mapper.Map<IEnumerable<EmployeeDto>>(data);
        }

        [Authorize]
        [HttpGet("calculate")]
        public IEnumerable<EmployeeDto> Get(DateTime date)
        {
            var data = EmpService.CalculateSalary(date);

            return Mapper.Map<IEnumerable<EmployeeDto>>(data);
        }
    }
}
