using gestion_congregacion.api.Features.Common;
using System.ComponentModel.DataAnnotations;

namespace gestion_congregacion.api.Features.Publishers
{
    public class Publisher : BaseDbModel
    {
        [Required]
        [MaxLength(200)]
        [MinLength(1)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(200)]
        [MinLength(1)]
        public string Surname { get; set; } = string.Empty;
    }
}
