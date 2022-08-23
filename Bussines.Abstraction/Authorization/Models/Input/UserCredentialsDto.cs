using System.ComponentModel.DataAnnotations;

namespace Bussines.Abstraction.Authorization.Models.Input
{
    public class UserCredentialsDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Incorect email format")]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;

    }
}
