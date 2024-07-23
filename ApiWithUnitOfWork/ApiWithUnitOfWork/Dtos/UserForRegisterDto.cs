using System.ComponentModel.DataAnnotations;

namespace ApiWithUnitOfWork.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 100 characters.")]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

    }
}
