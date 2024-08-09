using gestion_congregacion.api.Features.Common;
using gestion_congregacion.api.Features.MeetingEvents;
using System.ComponentModel.DataAnnotations;

namespace gestion_congregacion.api.Features.Publishers
{
    public class Publisher : BaseModel
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(200)]
        public string Surname { get; set; } = string.Empty;
    }
}
