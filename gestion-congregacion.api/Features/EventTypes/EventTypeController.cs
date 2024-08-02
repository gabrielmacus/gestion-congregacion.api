using gestion_congregacion.api.Features.Common;
using gestion_congregacion.api.Features.Operations;

namespace gestion_congregacion.api.Features.EventTypes
{
    public class EventTypeController : CRUDController<EventType>, IEventTypeController
    {
        public EventTypeController(
         ICreateOperation<EventType> createOperation,
         IReadOperation<EventType> readOperation,
         IUpdateOperation<EventType> updateOperation,
         IDeleteOperation<EventType> deleteOperation) : base(readOperation, createOperation, updateOperation, deleteOperation)
        {
        }
    }
    
}
