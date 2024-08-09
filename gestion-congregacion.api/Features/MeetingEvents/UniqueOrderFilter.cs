using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace gestion_congregacion.api.Features.MeetingEvents
{
    public class UniqueOrderFilter : IAsyncActionFilter
    {
        private readonly IMeetingEventRepository _eventRepository;

        public UniqueOrderFilter(IMeetingEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var evt = (MeetingEvent)context.ActionArguments["model"]!;
            var eventsCount = await _eventRepository
                    .GetQuery()
                    .CountAsync(i => i.MeetingId == evt.MeetingId );
            if (eventsCount ==  0) await next();
            context.Result = new BadRequestObjectResult("events.orderDuplicated");
        }
     

    }
}
