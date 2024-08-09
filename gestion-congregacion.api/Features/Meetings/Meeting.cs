using gestion_congregacion.api.Features.Common;
using gestion_congregacion.api.Features.MeetingEvents;
using System.ComponentModel.DataAnnotations;

namespace gestion_congregacion.api.Features.Meetings
{
    public class Meeting : BaseModel
    {
        [Required]
        public DateOnly WeekDate { get; set; }
        public List<MeetingEvent>? Events { get; set; }
    }
}
