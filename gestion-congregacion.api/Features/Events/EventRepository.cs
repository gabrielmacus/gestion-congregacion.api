using AutoMapper;
using gestion_congregacion.api.Features.Common;

namespace gestion_congregacion.api.Features.Events
{
    public class EventRepository : BaseRepository<Event, AppDbContext>, IEventRepository
    {
        public EventRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
