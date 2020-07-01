using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Employment;

namespace DAL.Context
{
    public static class DataSeeder
    {
        public static void Initialize(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasMany(p => p.Dependants).WithOne(p => p.Boss).IsRequired(false);

            modelBuilder.Entity<EmployeeType>().HasData(
                new EmployeeType
                {
                    Id = 1,
                    Name = "Employee",
                    YearAllowance = 3,
                    MaxAllowance = 30,
                    DependantsAllowance = 0
                },
                new EmployeeType
                {
                    Id = 2,
                    Name = "Manager",
                    YearAllowance = 5,
                    MaxAllowance = 40,
                    DependantsAllowance = 0.5
                },
                new EmployeeType
                {
                    Id = 3,
                    Name = "Salesman",
                    YearAllowance = 1,
                    MaxAllowance = 35,
                    DependantsAllowance = 0.3
                }
            );
        }
        public static void Initialize(UserManager<Employee> userManager, IDataContext context, RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Manager").Result)
            {
                roleManager.CreateAsync(new IdentityRole("Manager")).Wait();
            }
            if (!roleManager.RoleExistsAsync("Salesman").Result)
            {
                roleManager.CreateAsync(new IdentityRole("Salesman")).Wait();
            }
            if (!roleManager.RoleExistsAsync("Employee").Result)
            {
                roleManager.CreateAsync(new IdentityRole("Employee")).Wait();
            }
            var users = new List<UserInfo>()
            {
                new UserInfo
                {
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    UserName = "superUser",
                    Password = "Test1123456789",
                    Role = "Manager",
                    EmployType = 2,
                    Rate=10000,
                    WorkingYears = 9
                },
                new UserInfo
                {
                    FirstName = "Petr",
                    LastName = "Petrov",
                    UserName = "Petrov",
                    Password = "test2",
                    Role = "Manager",
                    EmployType = 2,
                    Rate=8000,
                    WorkingYears = 5
                },
                new UserInfo
                {
                    FirstName = "Sidor",
                    LastName = "Sidorovich",
                    UserName = "Sidor",
                    Password = "test3",
                    Role = "Salesman",
                    EmployType = 3,
                    Rate=6000,
                    WorkingYears = 40
                },
                new UserInfo
                {
                    FirstName = "Oleg",
                    LastName = "Olegov",
                    UserName = "Olegov",
                    Password = "test4",
                    Role = "Salesman",
                    EmployType = 3,
                    Rate=8000,
                    WorkingYears = 10
                },
                new UserInfo
                {
                    FirstName = "Ivan",
                    LastName = "Sidorovich",
                    UserName = "ISidor",
                    Password = "test5",
                    Role = "Employee",
                    EmployType = 1,
                    Rate=5000,
                    WorkingYears = 11
                },
                new UserInfo
                {
                    FirstName = "Sidor",
                    LastName = "Petrov",
                    UserName = "SPetrov",
                    Password = "test6",
                    Role = "Employee",
                    EmployType = 1,
                    Rate=4000,
                    WorkingYears = 3
                },
                new UserInfo
                {
                    FirstName = "Oleg",
                    LastName = "Ivanov",
                    UserName = "OIvanov",
                    Password = "test7",
                    Role = "Employee",
                    EmployType = 1,
                    Rate=3000,
                    WorkingYears = 0
                }
            };
            foreach (var user in users)
            {
                if (userManager.FindByNameAsync(user.UserName).Result == null)
                {
                    Employee us = new Employee
                    {
                        UserName = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        TypeId = user.EmployType,
                        BasicRate = user.Rate,
                        EmploymentDate = DateTime.Now.AddYears(-user.WorkingYears).AddDays(-15)

                    };

                    IdentityResult result = userManager.CreateAsync(us, user.Password.CreateMd5()).Result;

                    if (result.Succeeded) userManager.AddToRoleAsync(us, user.Role).Wait();
                }
            }

            var manager1 = context.Employees.Single(p => p.UserName == "superUser");
            var manager2 = context.Employees.Single(p => p.UserName == "Petrov");
            var sales1 = context.Employees.Single(p => p.UserName == "Sidor");
            var sales2 = context.Employees.Single(p => p.UserName == "Olegov");
            var emp1 = context.Employees.Single(p => p.UserName == "ISidor");
            var emp2 = context.Employees.Single(p => p.UserName == "SPetrov");
            var emp3 = context.Employees.Single(p => p.UserName == "OIvanov");
            emp3.BossId = sales2.Id;
            manager2.BossId = sales2.Id;
            sales2.BossId = manager1.Id;
            emp1.BossId = manager2.Id;
            emp2.BossId = manager1.Id;
            sales1.BossId = sales2.Id;
            context.Commit().Wait();
        }
    }
}
