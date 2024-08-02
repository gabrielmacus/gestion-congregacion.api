using gestion_congregacion.api.Features.Common;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace gestion_congregacion.api.Features.Roles
{
    public class Role : IdentityRole<long>, IBaseModel
    {
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
