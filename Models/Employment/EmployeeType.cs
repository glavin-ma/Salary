using System.Collections.Generic;
using Models.Classes;

namespace Models.Employment
{
    public class EmployeeType : BaseTypeEntity
    {
        public enum EmployeeTypes
        {
            Employee = 1,
            Manager = 2,
            Salesman = 3
        }
        public double YearAllowance { get; set; }
        public double MaxAllowance { get; set; }
        public double DependantsAllowance { get; set; }
    }
}
