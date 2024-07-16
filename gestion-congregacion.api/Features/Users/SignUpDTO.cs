using System.ComponentModel.DataAnnotations;

namespace gestion_congregacion.api.Features.Users
{
    public class SignUpDTO
    {
        [Required]
        [MaxLength(200)]
        public string Username { get; set; }
        [Required]
        [MaxLength(200)]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
