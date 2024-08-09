using gestion_congregacion.api.Features.Common;
using gestion_congregacion.api.Features.Meetings;
using gestion_congregacion.api.Features.Publishers;
using System.ComponentModel.DataAnnotations;

namespace gestion_congregacion.api.Features.MeetingEvents
{
    public class MeetingEvent : BaseModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public long? MeetingId { get; set; }
        public Meeting? Meeting { get; set; }
        public long? PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
        public long? HelperId { get; set; }
        public Publisher? Helper { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int? Order { get; set; }
        [Range(1, 50)]
        public int? Number { get; set; }
        [Required]
        public MeetingEventSection? Section { get; set; }
        [Required]
        [MaxLength(500)]
        public string? Title { get; set; }
        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [Range(1, 120)]
        public int? Duration { get; set; }



    }
}
