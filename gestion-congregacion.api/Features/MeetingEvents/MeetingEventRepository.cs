using AutoMapper;
using gestion_congregacion.api.Features.Common;

namespace gestion_congregacion.api.Features.MeetingEvents
{
    public class MeetingEventRepository : BaseRepository<MeetingEvent, AppDbContext>, IMeetingEventRepository
    {
        public MeetingEventRepository(AppDbContext context) : base(context)
        {
        }
    }
}
