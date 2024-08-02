using AutoMapper;
using gestion_congregacion.api.Features.Common;

namespace gestion_congregacion.api.Features.EventTypes
{
    public class EventTypeRepository : BaseRepository<EventType, AppDbContext>, IEventTypeRepository
    {
        public EventTypeRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

    }
}
