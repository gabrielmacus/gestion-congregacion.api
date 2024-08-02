using gestion_congregacion.api.Features.Common;
using gestion_congregacion.api.Features.Events;
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
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
