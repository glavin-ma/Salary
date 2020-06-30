using System;
using System.Collections.Generic;

namespace Models.Employment
{
    public class CalculatorData
    {
        public Employee Employee { get; set; }
        public bool Recursively { get; set; }
        public IList<Employee> EmpSalaries { get; set; }
        public DateTime CalculationDate { get; set; }
    }
}
