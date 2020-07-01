using Models.Employment;
using Services.Classes;

namespace Services.Calculation
{
    public class EmployeeCalculator : AbstractCalculator
    {
        public EmployeeCalculator(CalculatorData calcData) : base(calcData)
        {

        }
        public override double CalculateSalary()
        {
            var salary = CalcData.Employee.BasicRate + CalcData.Employee.CalculateAllowance(CalcData.CalculationDate);

            CalcData.Employee.Salary = NotAvailableSalary() ? 0 : salary;
            CalcData.EmpSalaries.Add(CalcData.Employee);
            return salary;
        }
    }
}
