using System;
using Models.Employment;

namespace Services.Classes
{
    public static class Extensions
    {
        public static double CalculateAllowance(this Employee employee, DateTime date)
        {
            var years = new DateTime((date - employee.EmploymentDate).Ticks).Year - 1;
            var percent = years * employee.Type.YearAllowance;
            return (percent > employee.Type.MaxAllowance ? employee.Type.MaxAllowance : percent) * employee.BasicRate / 100;
        }
    }
}
