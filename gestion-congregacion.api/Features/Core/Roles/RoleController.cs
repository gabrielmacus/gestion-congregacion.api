using AutoMapper;
using gestion_congregacion.api.Features.Common;
using gestion_congregacion.api.Features.Operations;
using gestion_congregacion.api.Features.Roles;

namespace gestion_congregacion.api.Features.Roles
{
    public class RoleController : CRUDController<Role>, IRoleController
    {
        public RoleController(
         ICreateOperation<Role> createOperation,
         IReadOperation<Role> readOperation,
         IUpdateOperation<Role> updateOperation,
         IDeleteOperation<Role> deleteOperation) : base(readOperation, createOperation, updateOperation, deleteOperation)
        {
        }
    }
}
