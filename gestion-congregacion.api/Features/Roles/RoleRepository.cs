using gestion_congregacion.api.Features.Common;

namespace gestion_congregacion.api.Features.Roles
{
    public class RoleRepository : BaseDbRepository<Role, AppDbContext>,IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
        }
    }
}
