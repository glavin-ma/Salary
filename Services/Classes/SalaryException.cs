using System;

namespace Services.Classes
{
    public class SalaryException : Exception
    {
        public SalaryException(string message) : base(message) { }
    }
}
