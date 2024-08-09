using gestion_congregacion.api.Features.Common;
using gestion_congregacion.api.Features.Operations;

namespace gestion_congregacion.api.Features.Meetings
{
    public class MeetingController : CRUDController<Meeting>, IMeetingController
    {
        public MeetingController(
    ICreateOperation<Meeting> createOperation,
    IReadOperation<Meeting> readOperation,
    IUpdateOperation<Meeting> updateOperation,
    IDeleteOperation<Meeting> deleteOperation) : base(readOperation, createOperation, updateOperation, deleteOperation)
        {
        }

    }
}
