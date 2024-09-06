using gestion_congregacion.api.Features.MeetingEvents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;

namespace gestion_congregacion.api.Features.FetchMeetingEvents
{
    [Route("/odata/[controller]")]
    public class FetchMeetingEventsController : ControllerBase
    {
        private readonly IFetchMeetingEventsServices _services;
        public FetchMeetingEventsController(IFetchMeetingEventsServices services)
        {
            _services = services;
        }

        [HttpGet("{weekDate}")]
        public async Task<ActionResult<MeetingEvent>> Get(DateOnly weekDate)
        {
            var events = await _services.FetchEventsFromJW(weekDate);
            return Ok(events);
        }
    }
}
