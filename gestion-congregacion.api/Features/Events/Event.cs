using gestion_congregacion.api.Features.Common;
using gestion_congregacion.api.Features.EventTypes;
using gestion_congregacion.api.Features.Publishers;
using System.ComponentModel.DataAnnotations;

namespace gestion_congregacion.api.Features.Events
{
    public class Event : BaseModel
    {
        [Required]
        public DateOnly WeekDate { get; set; }

        public long? PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
        [Required]
        public long EventTypeId { get; set; }
        public EventType? EventType { get; set; }

    }
}
