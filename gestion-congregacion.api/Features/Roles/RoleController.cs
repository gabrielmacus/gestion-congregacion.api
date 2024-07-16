using gestion_congregacion.api.Features.Common;

namespace gestion_congregacion.api.Features.Roles
{
    public class RoleController : CRUDController<Role, IRoleRepository>, IRoleController
    {
        public RoleController(IRoleRepository repository) : base(repository)
        {
        }
    }
}
