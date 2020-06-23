using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Employment;

namespace DAL.Context
{
    public class DataSeeder
    {
        public static void Initialize(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<IdentityRole>().HasData(
            //    new IdentityRole
            //    {
            //        Name = "SuperUser",
            //    }
            //);
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    FirstName = "Ivan",
                    LastName = "Ivanov"
                }
            );
        }
    }
}
