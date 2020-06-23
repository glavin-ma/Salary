using Microsoft.AspNetCore.Identity;

namespace Models.Employment
{
    public class Employee : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
