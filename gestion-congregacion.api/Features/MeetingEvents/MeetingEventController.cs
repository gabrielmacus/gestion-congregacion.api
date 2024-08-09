
using gestion_congregacion.api.Features.Common;
using gestion_congregacion.api.Features.MeetingEvents;
using gestion_congregacion.api.Features.Operations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace gestion_congregacion.api.Features.MeetingEvents
{
    public class MeetingEventController : CRUDController<MeetingEvent>, IMeetingEventController
    {
        public MeetingEventController(
              ICreateOperation<MeetingEvent> createOperation,
              IReadOperation<MeetingEvent> readOperation,
              IUpdateOperation<MeetingEvent> updateOperation,
              IDeleteOperation<MeetingEvent> deleteOperation) 
            : base(readOperation, createOperation, updateOperation, deleteOperation)
        {
        }

        [HttpPost]
        [ValidateModel]
        [ServiceFilter(typeof(UniqueOrderFilter))]
        public override async Task<IActionResult> Post(MeetingEvent model)
        {
            return await base.Post(model);
        }


    }
}
