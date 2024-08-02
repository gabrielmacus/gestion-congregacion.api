using System.ComponentModel.DataAnnotations;

namespace gestion_congregacion.api.Features.Common
{
    public class BaseModel : IBaseModel
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
