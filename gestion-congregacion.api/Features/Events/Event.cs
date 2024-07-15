using System.ComponentModel.DataAnnotations;

namespace gestion_congregacion.api.Features.Events
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
    }
}
