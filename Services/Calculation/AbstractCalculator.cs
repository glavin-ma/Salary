using System;
using System.Collections.Generic;
using System.Text;
using Models.Employment;

namespace Services.Calculation
{
    public abstract class AbstractCalculator
    {
        protected CalculatorData CalcData;
        protected AbstractCalculator(CalculatorData calcData)
        {
            CalcData = calcData;
        }
        public abstract double CalculateSalary();

        protected bool NotAvailableSalary()
        {
            return CalcData.CalculationDate < CalcData.Employee.EmploymentDate;
        }
    }
}
