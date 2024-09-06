using AutoMapper;
using gestion_congregacion.api.Features.Common;

namespace gestion_congregacion.api.Features.Meetings
{
    public class MeetingRepository : BaseRepository<Meeting, AppDbContext>,IMeetingRepository
    { 
        public MeetingRepository(AppDbContext context) : base(context)
        {
        }
    
    }
}
