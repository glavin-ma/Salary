using System.ComponentModel.DataAnnotations;

namespace DTO.Auth
{
    public class LoginDataDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string UserName { get; set; }
        [Required]
        [StringLength(35)]
        public string Password { get; set; }
    }
}
