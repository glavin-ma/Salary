using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Models.Employment
{
    public class Employee : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double BasicRate { get; set; }
        public DateTime EmploymentDate { get; set; }
        public string BossId { get; set; }
        public int TypeId { get; set; }
        [NotMapped]
        public double Salary { get; set; }

        [ForeignKey("TypeId")]
        public virtual EmployeeType Type { get; set; }
        [ForeignKey("BossId")]
        public virtual Employee Boss { get; set; }
        public virtual ICollection<Employee> Dependants { get; set; }
    }
}
