using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Employment;

namespace Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(string employeeId);
        IEnumerable<Employee> CalculateSalary(DateTime date);
    }
}
