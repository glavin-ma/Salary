using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DTO.Employment;
using Microsoft.AspNetCore.Mvc;
using Services.Classes;
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
        public async Task<IEnumerable<EmployeeDto>> Get()
        {
            var data= await EmpService.GetEmployees();
            var k = data.First().CalculateAllowance(DateTime.Now);
            return Mapper.Map<IEnumerable<EmployeeDto>>(data);
        } 
    }
}
