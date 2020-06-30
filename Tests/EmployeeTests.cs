using System;
using System.Collections.Generic;
using Models.Employment;
using NUnit.Framework;
using Services.Calculation;
using Services.Classes;

namespace Tests
{
    public class EmployeeTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void EmployeeExtension()
        {
            var emp = new Employee
            {
                BasicRate = 100,
                EmploymentDate = new DateTime(2010, 06, 30),
                Type = new EmployeeType()
                {
                    YearAllowance = 5,
                    MaxAllowance = 30
                }
            };
            var result = emp.CalculateAllowance(new DateTime(2020, 06, 30));
            Assert.That(result, Is.EqualTo(30).Within(.000000001));

            emp.EmploymentDate = new DateTime(2016, 06, 30);
            result = emp.CalculateAllowance(new DateTime(2020, 06, 30));
            Assert.That(result, Is.EqualTo(20).Within(.000000001));

            result = emp.CalculateAllowance(new DateTime(2016, 08, 30));
            Assert.That(result, Is.EqualTo(0).Within(.000000001));
        }

        [Test]
        public void CircularDependency()
        {
            var emp = new Employee
            {
                Id = "4",
                Dependants = new List<Employee>()
            };
            var emp1 = new Employee
            {
                Id = "3",
                Dependants = new List<Employee> { emp }
            };
            var emp2 = new Employee
            {
                Id = "2",
                Dependants = new List<Employee> { emp1 }
            };
            var result = CalculationHelper.HaveCircularDependency("1", new List<Employee> { emp2 });
            Assert.That(result, Is.EqualTo(false));

            emp.Dependants.Add(new Employee { Id = "1" });
            result = CalculationHelper.HaveCircularDependency("1", new List<Employee> { emp2 });
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void SalaryCalculator()
        {
            var emp = new Employee
            {
                BasicRate = 100,
                TypeId = 1,
                EmploymentDate = new DateTime(2016, 06, 30),
                Type = new EmployeeType
                {
                    MaxAllowance = 30,
                    YearAllowance = 5
                }
            };
            var data = new CalculatorData
            {
                CalculationDate = new DateTime(2020, 06, 30),
                Employee = emp,
                EmpSalaries = new List<Employee>()
            };
            var calculator = CalculationHelper.FactoryCalculator(data);
            var result = calculator.CalculateSalary();

            Assert.That(result, Is.EqualTo(120).Within(.000000001));

            var emp1 = new Employee
            {
                BasicRate = 100,
                TypeId = 2,
                EmploymentDate = new DateTime(2010, 06, 30),
                Type = new EmployeeType
                {
                    MaxAllowance = 30,
                    YearAllowance = 5,
                    DependantsAllowance = 8.333333333
                },
                Dependants = new List<Employee> { emp }
            };
            data = new CalculatorData
            {
                CalculationDate = new DateTime(2020, 06, 30),
                Employee = emp1,
                EmpSalaries = new List<Employee>()
            };
            calculator = CalculationHelper.FactoryCalculator(data);
            result = calculator.CalculateSalary();

            Assert.That(result, Is.EqualTo(140).Within(.000000001));

            var emp2 = new Employee
            {
                BasicRate = 100,
                TypeId = 3,
                EmploymentDate = new DateTime(2018, 06, 30),
                Type = new EmployeeType
                {
                    MaxAllowance = 30,
                    YearAllowance = 5,
                    DependantsAllowance = 10
                },
                Dependants = new List<Employee> { emp1 }
            };
            data = new CalculatorData
            {
                CalculationDate = new DateTime(2020, 06, 30),
                Employee = emp2,
                EmpSalaries = new List<Employee>()
            };
            calculator = CalculationHelper.FactoryCalculator(data);
            result = calculator.CalculateSalary();

            Assert.That(result, Is.EqualTo(136).Within(.000000001));
            Assert.That(data.EmpSalaries.Count, Is.EqualTo(3));

            emp.Dependants = new List<Employee> { emp2 };
            data = new CalculatorData
            {
                CalculationDate = new DateTime(2020, 06, 30),
                Employee = emp2,
                EmpSalaries = new List<Employee>()
            };
            calculator = CalculationHelper.FactoryCalculator(data);

            Assert.Throws<HandledException>(() => calculator.CalculateSalary());
        }
    }
}