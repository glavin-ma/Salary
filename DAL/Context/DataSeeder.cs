using System.Collections.Generic;
using System.Security.Cryptography;
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
            //modelBuilder.Entity<IdentityRole>().HasData(
            //    new IdentityRole
            //    {
            //        Name = "Employee"
            //    },
            //    new IdentityRole
            //    {
            //        Name = "Manager"
            //    },
            //    new IdentityRole
            //    {
            //        Name = "Salesman"
            //    }
            //);
        }

        private class UserInfo
        {
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
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
                    Role = "Manager"
                },
                new UserInfo
                {
                    FirstName = "Petr",
                    LastName = "Petrov",
                    UserName = "Petrov",
                    Password = "test2",
                    Role = "Manager"
                },
                new UserInfo
                {
                    FirstName = "Sidor",
                    LastName = "Sidorovich",
                    UserName = "Sidor",
                    Password = "test3",
                    Role = "Salesman"
                },
                new UserInfo
                {
                    FirstName = "Oleg",
                    LastName = "Olegov",
                    UserName = "Olegov",
                    Password = "test4",
                    Role = "Salesman"
                },
                new UserInfo
                {
                    FirstName = "Ivan",
                    LastName = "Sidorovich",
                    UserName = "ISidor",
                    Password = "test5",
                    Role = "Employee"
                },
                new UserInfo
                {
                    FirstName = "Sidor",
                    LastName = "Petrov",
                    UserName = "SPetrov",
                    Password = "test6",
                    Role = "Employee"
                },
                new UserInfo
                {
                    FirstName = "Oleg",
                    LastName = "Ivanov",
                    UserName = "OIvanov",
                    Password = "test7",
                    Role = "Employee"
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
                        LastName = user.LastName
                    };

                    IdentityResult result = userManager.CreateAsync(us, user.Password.CreateMd5()).Result;

                    if (result.Succeeded) userManager.AddToRoleAsync(us, user.Role).Wait();
                }
            }



        }
    }
}
