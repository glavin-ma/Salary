using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Models.Employment;
using Services.Calculation;
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

        public Employee GetEmployeeWithCalculation(string empId)
        {
            var emp = DataContext.Employees .Single(p => p.Id == empId);

            var salaryList = new List<Employee>();
            var data = new CalculatorData
            {
                CalculationDate = DateTime.Now,
                Employee = emp,
                EmpSalaries = salaryList
            };
            var calculator = CalculationHelper.FactoryCalculator(data);
            calculator.CalculateSalary();
            return data.Employee;
        }

        public IEnumerable<Employee> CalculateSalary(DateTime date)
        {
            var employees = GetEmployees().Result.Where(p => p.BossId == null);
            var salaryList = new List<Employee>();
            foreach (var emp in employees)
            {
                var data = new CalculatorData
                {
                    CalculationDate = date,
                    Employee = emp,
                    EmpSalaries = salaryList
                };
                var calculator = CalculationHelper.FactoryCalculator(data);
                calculator.CalculateSalary();
            }
            if (salaryList.Count != DataContext.Employees.Count()) throw new HandledException("Circular dependency error.");
            return salaryList;
        }
    }
}
