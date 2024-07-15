using System.ComponentModel.DataAnnotations;

namespace gestion_congregacion.api.Features.Common
{
    public class BaseDbModel
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
