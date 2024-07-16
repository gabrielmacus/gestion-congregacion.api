using gestion_congregacion.api.Features.Common;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace gestion_congregacion.api.Features.Users
{
    public class User : IdentityUser<long>, IBaseDbModel
    {
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [MaxLength(200)]
        public string? FirstName { get; set; }

        [MaxLength(200)]
        public string? LastName { get; set; }
    }
}
