using System.Linq;
using Models.Employment;
using Services.Classes;

namespace Services.Calculation
{
    public class ManagerCalculator : AbstractCalculator
    {
        public ManagerCalculator(CalculatorData calcData) : base(calcData) { }
        public override double CalculateSalary()
        {
            var check = CalculationHelper.HaveCircularDependency(CalcData.Employee.Id, CalcData.Employee.Dependants);
            if (check) throw new HandledException("Circular dependency error.");
            double dependantsSum = 0;
            double dependantsSumRec = 0;
            if (CalcData.Employee.Dependants != null)
                foreach (var emp in CalcData.Employee.Dependants)
                {
                    var item = CalcData.EmpSalaries.SingleOrDefault(p => p.Id == emp.Id);
                    if (item != null)
                    {
                        dependantsSum += item.Salary;
                        continue;
                    }
                    var data = new CalculatorData
                    {
                        CalculationDate = CalcData.CalculationDate,
                        Employee = emp,
                        EmpSalaries = CalcData.EmpSalaries,
                        Recursively = CalcData.Recursively
                    };
                    var calculator = CalculationHelper.FactoryCalculator(data);
                    dependantsSumRec += calculator.CalculateSalary();
                    dependantsSum += data.EmpSalaries.Single(p => p.Id == emp.Id).Salary;
                }

            double salary = CalcData.Employee.BasicRate + CalcData.Employee.CalculateAllowance(CalcData.CalculationDate) + dependantsSum * CalcData.Employee.Type.DependantsAllowance / 100;
            CalcData.Employee.Salary = NotAvailableSalary() ? 0 : salary;
            CalcData.EmpSalaries.Add(CalcData.Employee);
            return CalcData.Recursively ? salary + dependantsSumRec : salary;
        }
    }
}
