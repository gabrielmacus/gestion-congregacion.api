using AutoMapper;
using gestion_congregacion.api.Features.Common;

namespace gestion_congregacion.api.Features.Roles
{
    public class RoleRepository : BaseRepository<Role, AppDbContext>,IRoleRepository
    {
        public RoleRepository(AppDbContext context, 
            IMapper mapper) : base(context, mapper)
        {
        }
    }
}
