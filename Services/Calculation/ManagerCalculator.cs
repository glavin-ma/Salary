using System;
using Models.Employment;
using Services.Classes;

namespace Services.Calculation
{
    public class ManagerCalculator : AbstractCalculator
    {
        public ManagerCalculator(Employee emp) : base(emp)
        {

        }
        public override double CalculateSalary(DateTime date)
        {
            return Emp.BasicRate + Emp.CalculateAllowance(date);
        }
    }
}
