using System;
using System.Collections.Generic;
using System.Text;
using Models.Employment;
using Services.Classes;

namespace Services.Calculation
{
    class SalesmanCalculator : AbstractCalculator
    {
        public SalesmanCalculator(Employee emp) : base(emp)
        {

        }
        public override double CalculateSalary(DateTime date)
        {
            return Emp.BasicRate + Emp.CalculateAllowance(date);
        }
    }
}
