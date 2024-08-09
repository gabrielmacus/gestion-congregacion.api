using gestion_congregacion.api.Features.MeetingEvents;

namespace gestion_congregacion.api.Features.FetchMeetingEvents
{
    public interface IFetchMeetingEventsServices
    {
        Task SaveEventsFromJW();
        Task<string> GetFetchScript();
        Task<List<MeetingEvent>> FetchEventsFromJW(DateOnly weekDay);
        int GetWeekOfYear(DateOnly date);
    }
}
