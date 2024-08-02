
using gestion_congregacion.api.Features.Common;
using gestion_congregacion.api.Features.Events;
using gestion_congregacion.api.Features.Operations;

namespace gestion_congregacion.api.Features.Events
{
    public class EventController : CRUDController<Event>, IEventController
    {
        public EventController(
              ICreateOperation<Event> createOperation,
              IReadOperation<Event> readOperation,
              IUpdateOperation<Event> updateOperation,
              IDeleteOperation<Event> deleteOperation) 
            : base(readOperation, createOperation, updateOperation, deleteOperation)
        {
        }


    }
}
