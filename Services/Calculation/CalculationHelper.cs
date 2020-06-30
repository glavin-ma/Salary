using System.Collections.Generic;
using Models.Employment;

namespace Services.Calculation
{
    public class CalculationHelper
    {

        public static AbstractCalculator FactoryCalculator(CalculatorData calcData)
        {
            AbstractCalculator calc = null;
            switch (calcData.Employee.TypeId)
            {
                case (int)EmployeeType.EmployeeTypes.Employee:
                    calc = new EmployeeCalculator(calcData);
                    break;
                case (int)EmployeeType.EmployeeTypes.Manager:
                    calc = new ManagerCalculator(calcData);
                    break;
                case (int)EmployeeType.EmployeeTypes.Salesman:
                    calc = new SalesmanCalculator(calcData);
                    break;
            }
            return calc;
        }

        public static bool HaveCircularDependency(string id, IEnumerable<Employee> empList)
        {
            var result = false;
            if (empList != null)
                foreach (var emp in empList)
                {
                    if (emp.Id == id || HaveCircularDependency(id, emp.Dependants))
                    {
                        result = true;
                        break;
                    }
                }
            return result;
        }
    }
}
