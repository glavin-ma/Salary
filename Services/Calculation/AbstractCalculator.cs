using System;
using System.Collections.Generic;
using System.Text;
using Models.Employment;

namespace Services.Calculation
{
    public abstract class AbstractCalculator
    {
        protected Employee Emp;
        protected AbstractCalculator(Employee emp)
        {
            Emp = emp;
        }
        public abstract double CalculateSalary(DateTime date);
    }
}
