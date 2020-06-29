using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Models.Employment;
using Services.Classes;
using Services.Interfaces;

namespace Services.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        public EmployeeService(IDataContext context)
        {
            DataContext = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await DataContext.Employees.Include(p => p.Type).ToListAsync();
        }
        public async Task<Employee> GetEmployee(string employeeId)
        {
            return await DataContext.Employees.SingleAsync(p => p.Id == employeeId);
        }
    }
}
