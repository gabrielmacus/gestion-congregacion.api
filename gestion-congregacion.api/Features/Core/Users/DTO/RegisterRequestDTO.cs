using System.ComponentModel.DataAnnotations;

namespace gestion_congregacion.api.Features.Users.DTO
{
    public class RegisterRequestDTO
    {
        [Required]
        [MaxLength(200)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [MaxLength(200)]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? FirstName { get; set; }

        [MaxLength(200)]
        public string? LastName { get; set; }
    }
}
