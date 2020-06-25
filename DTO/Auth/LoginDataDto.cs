using System.ComponentModel.DataAnnotations;

namespace DTO.Auth
{
    public class LoginDataDto
    {
        [Required(ErrorMessage = "test message")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
