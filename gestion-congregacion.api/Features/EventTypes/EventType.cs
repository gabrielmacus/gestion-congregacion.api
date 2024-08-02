using gestion_congregacion.api.Features.Common;
using System.ComponentModel.DataAnnotations;

namespace gestion_congregacion.api.Features.EventTypes
{
    public class EventType : BaseModel
    {
        [Required]
        [MaxLength(300)]
        public string Name { get; set; } = string.Empty;
    }
}
