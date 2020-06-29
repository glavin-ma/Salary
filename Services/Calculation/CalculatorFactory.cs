using Models.Employment;

namespace Services.Calculation
{
    public class CalculatorFactory
    {
        private readonly Employee _emp;
        public CalculatorFactory(Employee emp)
        {
            _emp = emp;
        }

        public AbstractCalculator GetCalculator()
        {
            AbstractCalculator calc = null;
            switch (_emp.TypeId)
            {
                case (int)EmployeeType.EmployeeTypes.Employee:
                    calc = new EmployeeCalculator(_emp);
                    break;
                case (int)EmployeeType.EmployeeTypes.Manager:
                    calc = new ManagerCalculator(_emp);
                    break;
                case (int)EmployeeType.EmployeeTypes.Salesman:
                    calc = new SalesmanCalculator(_emp);
                    break;
            }
            return calc;
        }
    }
}
